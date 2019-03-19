using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Security;

namespace Frends.Community.PowerShell
{
    internal class TaskUserInterface : PSHostUserInterface
    {
        public override string ReadLine()
        {
            return ""; // We cannot provide user input
        }

        public override SecureString ReadLineAsSecureString()
        {
            return new SecureString();
        }

        public override void Write(string value)
        {
            Trace.Write(value);
        }

        public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
        {
            Write(value);
        }

        public override void WriteLine(string value)
        {
            Trace.WriteLine(value);//_messages.AppendLine(value);
        }

        public override void WriteErrorLine(string value)
        {
            Trace.WriteLine(value);
        }

        public override void WriteDebugLine(string message)
        {
            Trace.WriteLine(message);
        }

        public override void WriteProgress(long sourceId, ProgressRecord record)
        {
            // We don't handle progress
        }

        public override void WriteVerboseLine(string message)
        {
            Trace.WriteLine(message);
        }

        public override void WriteWarningLine(string message)
        {
            Trace.WriteLine(message);
        }

        public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
        {
            return descriptions?.ToDictionary(desc => desc.Name, desc => new PSObject("")); // Return empty string as answer
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName)
        {
            throw new NotImplementedException("We cannot prompt for user credentials");
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName,
            PSCredentialTypes allowedCredentialTypes, PSCredentialUIOptions options)
        {
            throw new NotImplementedException("We cannot prompt for user credentials");
        }

        public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice)
        {
            //always go by default
            return defaultChoice;
        }

        private readonly PSHostRawUserInterface _rawUserInterface = null; // Not implemented

        public override PSHostRawUserInterface RawUI
        {
            get { return _rawUserInterface; }
        }
    }

    /// <summary>
    /// This is a sample implementation of the PSHost abstract class for 
    /// console applications. Not all members are implemented. Those that 
    /// are not implemented throw a NotImplementedException exception or 
    /// return nothing.
    /// </summary>
    internal class TaskPowershellHost : PSHost
    {
        private readonly TaskUserInterface _userInterface = new TaskUserInterface();

        /// <summary>
        /// The culture information of the thread that created
        /// this object.
        /// </summary>
        private readonly CultureInfo _originalCultureInfo =
            System.Threading.Thread.CurrentThread.CurrentCulture;

        /// <summary>
        /// The UI culture information of the thread that created
        /// this object.
        /// </summary>
        private readonly CultureInfo _originalUiCultureInfo =
            System.Threading.Thread.CurrentThread.CurrentUICulture;

        /// <summary>
        /// The identifier of this PSHost implementation.
        /// </summary>
        private readonly Guid _myId = Guid.NewGuid();

        /// <summary>
        /// Initializes a new instance of the MyHost class. Keep
        /// a reference to the host application object so that it 
        /// can be informed of when to exit.
        /// </summary>
        public TaskPowershellHost()
        {
        }

        /// <summary>
        /// Return the culture information to use. This implementation 
        /// returns a snapshot of the culture information of the thread 
        /// that created this object.
        /// </summary>
        public override CultureInfo CurrentCulture
        {
            get { return _originalCultureInfo; }
        }

        /// <summary>
        /// Return the UI culture information to use. This implementation 
        /// returns a snapshot of the UI culture information of the thread 
        /// that created this object.
        /// </summary>
        public override CultureInfo CurrentUICulture
        {
            get { return _originalUiCultureInfo; }
        }

        /// <summary>
        /// This implementation always returns the GUID allocated at 
        /// instantiation time.
        /// </summary>
        public override Guid InstanceId
        {
            get { return _myId; }
        }

        /// <summary>
        /// Return a string that contains the name of the host implementation. 
        /// Keep in mind that this string may be used by script writers to
        /// identify when your host is being used.
        /// </summary>
        public override string Name
        {
            get { return "SimpleTaskPowershellHost"; }
        }


        /// <summary>
        /// This sample does not implement a PSHostUserInterface component so
        /// this property simply returns null.
        /// </summary>
        public override PSHostUserInterface UI
        {
            get { return _userInterface; }
        }

        /// <summary>
        /// Return the version object for this application. Typically this
        /// should match the version resource in the application.
        /// </summary>
        public override Version Version
        {
            get { return new Version(1, 0, 0, 0); }
        }

        public override void SetShouldExit(int exitCode)
        {
            
        }

        /// <summary>
        /// Not implemented by this example class. The call fails with
        /// a NotImplementedException exception.
        /// </summary>
        public override void EnterNestedPrompt()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// Not implemented by this example class. The call fails
        /// with a NotImplementedException exception.
        /// </summary>
        public override void ExitNestedPrompt()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        /// <summary>
        /// This API is called before an external application process is 
        /// started. Typically it is used to save state so the parent can 
        /// restore state that has been modified by a child process (after 
        /// the child exits). In this example, this functionality is not  
        /// needed so the method returns nothing.
        /// </summary>
        public override void NotifyBeginApplication()
        {
            return;
        }

        /// <summary>
        /// This API is called after an external application process finishes.
        /// Typically it is used to restore state that a child process may
        /// have altered. In this example, this functionality is not  
        /// needed so the method returns nothing.
        /// </summary>
        public override void NotifyEndApplication()
        {
            return;
        }

    }
}
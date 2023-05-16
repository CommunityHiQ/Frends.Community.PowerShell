using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#pragma warning disable 1591

namespace Frends.Community.PowerShell
{

    public class RunCommandInput
    {
        /// <summary>
        /// The PowerShell command to execute 
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Parameters for the command, provided switch parameters need to have a boolean value
        /// </summary>
        public PowerShellParameter[] Parameters { get; set; }

        /// <summary>
        /// Should the information stream be logged. If false, log will be an empty string.
        /// If set to true, a lot of string data may be logged. Use with caution.
        /// </summary>
        public bool LogInformationStream { get; set; }
    }

    public class PowerShellParameter
    {
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "Text")]
        public object Value { get; set; }
    }

    public class RunScriptInput
    {
        /// <summary>
        ///  Parameters for the script, provided switch parameters need to have a boolean value
        /// </summary>
        public PowerShellParameter[] Parameters { get; set; }

        /// <summary>
        /// Should the script be read from a file or from the Script parameter
        /// </summary>
        public bool ReadFromFile { get; set; }

        /// <summary>
        /// Location for the script file
        /// </summary>
        [UIHint(nameof(ReadFromFile), "", true)]
        public string ScriptFilePath { get; set; }

        /// <summary>
        /// The script to execute 
        /// </summary>
        [UIHint(nameof(ReadFromFile), "", false)]
        public string Script { get; set; }

        /// <summary>
        /// Should the information stream be logged. If false, log will be an empty string.
        /// If set to true, a lot of string data may be logged. Use with caution.
        /// </summary>
        public bool LogInformationStream { get; set; }
    }

    public class RunOptions
    {
        [DefaultValue(null)]
        [DisplayFormat(DataFormatString = "Expression")]
        public SessionWrapper Session { get; set; }
    }

    public class PowerShellResult
    {
        public IList<dynamic> Result { get; set; }
        public IList<string> Errors { get; set; }
        public string Log { get; set; }
    }
}

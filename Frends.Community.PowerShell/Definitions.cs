using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Frends.Community.PowerShell;

namespace Frends.Community.PowerShell
{
    public class RunCommandInput {
        public string Command { get; set; }
        public PowerShellParameter[] Parameters { get; set; }
    }

    public class PowerShellParameter
    {
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "Text")]
        public object Value { get; set; }
    }

    public class RunScriptInput
    {
        public PowerShellParameter[] Parameters { get;set; }
        public bool ReadFromFile { get; set; }

        [UIHint(nameof(ReadFromFile), "", true)]
        public string ScriptFilePath { get; set; }

        [UIHint(nameof(ReadFromFile), "", false)]
        public string Script { get; set; }

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

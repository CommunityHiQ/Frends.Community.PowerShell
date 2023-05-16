using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Frends.Community.PowerShell.Tests
{
    [TestFixture]
    public class PowerShellTests
    {
        [Test]
        public void RunCommand_ShouldRunCommandWithParameter()
        {
            var result = PowerShell.RunCommand(new RunCommandInput
            {
                Command = "New-TimeSpan",
                Parameters = new[]
                {
                    new PowerShellParameter
                    {
                        Name = "Hours",
                        Value = "1"
                    },
                },
                LogInformationStream = true
            },
                new RunOptions(),
                default);

            Assert.IsNotNull(result.Result);
            Assert.AreEqual(TimeSpan.FromHours(1), result.Result.Single());
        }

        [Test]
        public void RunScript_ShouldRunScriptWithParameter()
        {
            var script = @"param([string]$testParam)
$testParam
write-output ""my test param: $testParam""";

            var result = PowerShell.RunScript(new RunScriptInput
            {
                Parameters = new[] { new PowerShellParameter { Name = "testParam", Value = "my test param" } },
                ReadFromFile = false,
                Script = script,
                LogInformationStream = true
            }, new RunOptions(),
                default);

            Assert.AreEqual(2, result.Result.Count);
            Assert.AreEqual("my test param: my test param", result.Result.Last());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void RunCommand_ShouldRunCommandWithSwitchParameter(object switchParameterValue)
        {
            var session = PowerShell.CreateSession();
            session.PowerShell.AddScript(@"
function Test-Switch { 
    param([switch] $switchy) 
    $switchy.IsPresent 
}", false);
            session.PowerShell.Invoke();
            session.PowerShell.Commands.Clear();

            var result = PowerShell.RunCommand(new RunCommandInput
            {
                Command = "Test-Switch",
                Parameters = new[]
                    {
                        new PowerShellParameter
                        {
                            Name = "switchy",
                            Value = switchParameterValue
                        }
                    },
                LogInformationStream = true
            },
                new RunOptions
                {
                    Session = session
                },
                default);

            Assert.AreEqual(switchParameterValue, result.Result.Single());
        }


        private readonly string script =
            @"
new-timespan -hours 1
new-timespan -hours 2";

        [Test]
        public void RunScript_ShouldRunScriptFromFile()
        {
            var scriptFilePath = Path.GetTempFileName();
            PowerShellResult result;
            try
            {
                File.WriteAllText(scriptFilePath, script);
                result = PowerShell.RunScript(new RunScriptInput
                {
                    ReadFromFile = true,
                    ScriptFilePath = scriptFilePath,
                    LogInformationStream = true
                }, new RunOptions(), default);
            }
            finally
            {
                File.Delete(scriptFilePath);
            }

            Assert.AreEqual(2, result.Result.Count);
            Assert.AreEqual(TimeSpan.FromHours(2), result.Result.Last());
        }

        [Test]
        public void RunScript_ShouldRunScriptFromParameter()
        {
            PowerShellResult result;

            result = PowerShell.RunScript(new RunScriptInput
            {
                ReadFromFile = false,
                Script = script,
                LogInformationStream = true
            }, new RunOptions(), default);


            Assert.AreEqual(TimeSpan.FromHours(2), result.Result.Last());
        }

        [Test]
        public void RunCommandAndScript_ShouldUseSharedSession()
        {
            var session = PowerShell.CreateSession();

            var result2 = PowerShell.RunScript(new RunScriptInput
            {
                ReadFromFile = false,
                Script = "(new-timespan -hours 1) + $timespan",
                LogInformationStream = true
            },
                new RunOptions
                {
                    Session = session
                }, default);

            Assert.AreEqual(TimeSpan.FromHours(2), result2.Result.Single());
        }

        [Test]
        public void RunScript_ShouldListErrors()
        {
            var script =
@"
This-DoesNotExist
$Source = @""
using System; 
namespace test {
    public static class pstest {
        public static void test`(`) {
        throw new Exception(""Argh""); 
        }
    }
}
""@

Add-Type -TypeDefinition $Source -Language CSharp
[test.pstest]::test()
get-process -name doesnotexist -ErrorAction Stop
";

            var resultError = Assert.Throws<Exception>(() => PowerShell.RunScript(new RunScriptInput { ReadFromFile = false, Script = script, LogInformationStream = true }, null, default));

            Assert.IsNotNull(resultError.Message);
        }

        [Test]
        public void RunScript_ShouldOutputCustomPowershellObjects()
        {
            var script =
@"$test = New-Object pscustomobject
$test | Add-Member -type NoteProperty -name Property1 -Value 'Value1'
$test | Add-Member -type NoteProperty -name Property2 -Value 'Value2'
$test
";
            var result = PowerShell.RunScript(new RunScriptInput
            {
                ReadFromFile = false,
                Script = script,
                LogInformationStream = true
            }, null, default);

            Assert.AreEqual("Value1", result.Result[0].Property1);
            Assert.AreEqual("Value2", result.Result[0].Property2);
        }
    }
}

- [Frends.Community.PowerShell](#frendspowershell)
  - [Installing](#installing)
  - [Building](#building)
  - [Contributing](#contributing)
  - [Documentation](#documentation)
    - [PowerShell.RunCommand](#powershellruncommand)
      - [Input](#input)
      - [Result](#result)
    - [PowerShell.RunScript](#powershellrunscript)
      - [Input](#input)
      - [Result](#result)
  - [License](#license)

# Frends.Community.PowerShell

## Installing
You can install the task via FRENDS UI Task View or you can find the nuget package from the following nuget feed
`https://www.myget.org/F/frends/api/v2`

## Building

Clone a copy of the repo

`git clone https://github.com/FrendsPlatform/Frends.Community.PowerShell.git`


Restore dependencies

`nuget restore Frends.Community.PowerShell`

Rebuild the project

Run Tests with nunit3. Tests can be found under

`Frends.Community.PowerShell.Tests\bin\Release\Frends.Community.PowerShell.Tests.dll`

Create a nuget package

`nuget pack nuspec/Frends.Community.PowerShell.nuspec`

## Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

## Documentation

The PowerShell Tasks require PowerShell 5.0 to work

### PowerShell.RunCommand 

Execute a single PowerShell command with parameters, the Task fails when a terminating error is encountered or an error is thrown

#### Input

| Property          | Type                               | Description                                                                         | Example                    |
| ----------------- | ---------------------------------- | ----------------------------------------------------------------------------------- | -------------------------- |
| Command           | string                             | The PowerShell command to execute                                                   | `New-TimeSpan`             |
| Parameters        | Array{Name: string, Value: object} | Parameters for the command, provided switch parameters need to have a boolean value | `Name = Hours, Value = 1`  |


#### Result
{Result: Array{object}, Errors: Array{string}, Log: string}

### PowerShell.RunScript

Run a PowerShell script with parameters, the Task fails when a terminating error is encountered or an error is thrown

#### Input

| Property          | Type                               | Description                                                                             | Example                                                  |
| ----------------- | ---------------------------------- | --------------------------------------------------------------------------------------- | -------------------------------------------------------- |
| ReadFromFile      | bool                               | Should the script be read from a file or from the Script parameter                      | `true`                                                   |
| ScriptFilePath    | string                             | Location for the script file                                                            | `F:\myScript.ps1`                                        |
| Script            | string                             | The script to execute                                                                   | `New-TimeSpan -Hours 1 \| convertto-json`                |
| Parameters        | Array{Name: string, Value: object} | Parameters for the script, provided switch parameters need to have a boolean value      | `Name = Hours, Value = 1`                                |


#### Result
{Result: Array{object}, Errors: Array{string}, Log: string}


## License
This project is licensed under the MIT License - see the LICENSE file for details

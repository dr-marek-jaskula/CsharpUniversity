namespace CsharpAdvanced.Command_Line;

public class CreateCliTool
{
    //Use the "Script" custom Template (it is created with all required settings) and follow steps

    //# CLI Tool Guide
    //## How to create a global tool?
    //1. Open PowerShell or Command Line Prompt
    //2. Navigate to the directory where the script is
    //3. Use command "docker pack"
    
    //This will result in creating a "NuGetPackage" directory in which the packed tool is present.
    //4. Use "dotnet tool install --global --add-source ./NuGetPackage CSharpScripting
    
    //## How to create a local tool?
    //1. Follow the steps from previous guide
    //2. Use "dotnet new tool-manifest #if you are settong up this repo
    //3. Use "dotnet tool install --local --add-source ./NuGetPackage CSharpScripting

    //## How to change command name?
    //1. Open.csproj file
    //2. Change the content of the<ToolCommandName> SomeContent</ToolCommandName> to the desired command
}
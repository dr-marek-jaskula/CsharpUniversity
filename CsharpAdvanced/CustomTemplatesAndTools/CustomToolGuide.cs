namespace CsharpAdvanced.CustomTemplatesAndTools;

public class CustomToolGuide
{
    //## How to create a global tool?

    //1. Open PowerShell or Command Line Prompt
    //2. Navigate to the directory where the script is
    //3. Use command "dotnet pack"
    //This will result in creating a "NuGetPackage" directory in which the packed tool is present.
    //4. Use "dotnet tool install --global --add-source ./NuGetPackage <ToolName>

    //## How to create a local tool?

    //1. Follow the steps from previous guide
    //2. Use "dotnet new tool-manifest" #if you are setting up this repo
    //3. Use "dotnet tool install --local --add-source ./NuGetPackage <ToolName>

    //## How to change command name?

    //1. Open .csproj file
    //2. Change the content of the <ToolCommandName>SomeContent</ToolCommandName> to the desired command

    //## How to uinstall tool?

    //1. Get tool list using "dotnet tool list --global" 
    //2. Use "dotnet tool uninstall <PackageId> --global"
}
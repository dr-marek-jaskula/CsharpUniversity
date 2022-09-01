# Custom Tool Guide

## How to create a global tool?

- Open PowerShell or Command Line Prompt
- Navigate to the directory where the script is
- Use command

```cmd
dotnet pack
```

This will result in creating a "NuGetPackage" directory in which the packed tool is present.

```cmd
dotnet tool install --global --add-source ./NuGetPackage <ToolName>
```

## How to create a local tool?

- Follow the steps from previous guide
- Then use (if you are setting up this repo)
  
```cmd
dotnet new tool-manifest
```

- Finally use

```cmd
dotnet tool install --local --add-source ./NuGetPackage <ToolName>
```

## How to change command name?

- Open .csproj file
- Change the content of the \<ToolCommandName\>SomeContent\</ToolCommandName\> to the desired command

## How to uinstall tool?

- Get tool list using

```cmd
dotnet tool list --global
```

- Use

```cmd
dotnet tool uninstall <PackageId> --global
```

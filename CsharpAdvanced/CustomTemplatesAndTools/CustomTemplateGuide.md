# Custom Project Templates

## Create Custom Project Template

- Use Visual Studio Code (it is just better for configuring)
- On the project level, add folder called ".template.config"
- Inside this folder add file "template.json" (this file is described below)
- Open terminal and go to the project folder
- Design the custom template as desired (! see below)
- Use command "dotnet new --install ." (we can point to the NuGet Package or to the directory, here we point to the current folder)

Custom template is installed. To uninstall the template we use: 
```cmd
dotnet new --uninstall .
```

To check the template we can 
```cmd 
dotnet new --list
dotnet new MaximalApi --help
``` 

To create a project we can use Visual Studio 2022. Sometimes VS2022 needs some time to see a new template, so for fast test use:
```cmd
dotnet new MaximalApi
```

We can use it with example bool param:
```cmd
dotnet new MaximalApi -I "false"
```

## Make NuGet Package for a template 

- Create a new, additional project file called for example "MyCustomTemplates.csproj"

```csproj
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
	  <PackageType>Template</PackageType>
	  <PackageVersion>1.0</PackageVersion>
	  <PackageId>WebApiTemplate</PackageId>
	  <Title>WebApi Full Template</Title>
	  <Authors>dr Marek Jaskuła</Authors>
	  <Description>"WebApi Full Template</Description>
	  <PackageTags>dotnet-new;templates</PackageTags>
	  <TargetFramework>net6.0</TargetFramework>

	  <IncludeContentInPack>true</IncludeContentInPack>
	  <IncludeBuildOutput>false</IncludeBuildOutput>
	  <ContentTargetFolders>content</ContentTargetFolders>
  </PropertyGroup>

	<ItemGroup>
		<Content Include="templates\**\*" Exclude="templates\**\bin\**;temp"/>
		<Compile Remove="**\*" />
	</ItemGroup>

</Project>
```

The file should be created: 
- MyCustomTemplates.csproj should in the same place where is "templates" folder (we should create such separate folder)
- In the "templates" folder we should have folder with the templates like "WebApiTemplate"

Finally we call command:
```cmd
dotnet pack
```

## template.json design

In order to design the custom template in a desired way we need to:
- Design the "template.json" file

```json
{
    "$schema": "https://json.schemastore.org/template",
    "author": "dr Marek Jaskuła",
    "classifications": ["Web", "WebApi"],
    "identity": "WebApiTemplate",
    "name": "Maximal WebApi Template",
    "shortName": "MaximalApi",
    "sourceName": "WebApiTemplate",
    "tags": {
        "language": "C#",
        "type": "project"
    },
    "preferNameDirectory": true,
    "symbols": {
        "TargetFramework": {
            "type": "parameter",
            "description": "The target framework for the project",
            "datatype": "choice",
            "choices": [
                {
                    "choice": "net6.0"
                }
            ],
            "defaultValue": "net6.0",
            "replaces": "$TargetFramework$",
            "displayName": "Target Framework"
        },
        "AddSwaggerSupport": {
    "type": "parameter",
            "description": "Add swagger support",
            "datatype": "bool",
            "defaultValue": "true",
            "displayName": "Swagger Support"
        },
        "AddHealthChecks": {
    "type": "parameter",
            "description": "Add health checks support for API and SqlServer with UI",
            "datatype": "bool",
            "defaultValue": "false",
            "displayName": "Health Checks"
        },
        "AddLinq2dbBulkOperations": {
    "type": "parameter",
            "description": "Add library for Bulk Operations like Bulk Update or Bulk Delete",
            "datatype": "bool",
            "defaultValue": "true",
            "displayName": "Bulk Operations"
        },
        "AddLazyLoading": {
    "type": "parameter",
            "description": "Add data LazyLoading",
            "datatype": "bool",
            "defaultValue": "false",
            "displayName": "Lazy Loading"
        },
        "AddPolly": {
    "type": "parameter",
            "description": "Add Polly",
            "datatype": "bool",
            "defaultValue": "true",
            "displayName": "Polly Policies"
        },
        "AddSerilog": {
    "type": "parameter",
            "description": "Add Serilog and Seq configurations",
            "datatype": "bool",
            "defaultValue": "true",
            "displayName": "Serilog"
        },
        "AddSymSpell": {
    "type": "parameter",
            "description": "Add SymSpell library for string approximation",
            "datatype": "bool",
            "defaultValue": "false",
            "displayName": "SymSpell"
        },
        "AddHttpClient": {
    "type": "parameter",
            "description": "Add sample Http Client as GitHubUser",
            "datatype": "bool",
            "defaultValue": "true",
            "displayName": "HttpClient"
        },
        "AddReadMe": {
    "type": "parameter",
            "description": "Add ReadMe.md file",
            "datatype": "bool",
            "defaultValue": "true",
            "displayName": "ReadMe.md"
        }
    },
    "sources": [{
        "modifiers": [{
                "exclude": [
                    "**/[Bb]in/**",
                    "**/[Oo]bj/**",
                    ".template.config/**/*",
                    "**/*.filelist",
                    "**/*.user",
                    "**/*.lock.json",
                    "**/.vs/**"
                ]
            },
            {
    "condition": "(!AddSwaggerSupport)",
                "exclude": [
                    "**/wwwroot/swaggerstyles/**",
                    "**/Swagger/**",
                    "**/Registration/SwaggerRegistration.cs"
                ]
            },
            {
    "condition": "(!AddHealthChecks)",
                "exclude": [
                    "**/HealthChecks/**"
                ]
            },
            {
    "condition": "(!AddPolly)",
                "exclude": [
                    "**/Polly/**"
                ]
            },
            {
    "condition": "(!AddHttpClient)",
                "exclude": [
                    "**/Registration/HttpClientRegistration.cs",
                    "**/Models/GitHubUser.cs"
                ]
            },
            {
    "condition": "(!AddSymSpell)",
                "exclude": [
                    "**/Registration/SymSpellRegistration.cs",
                    "**/Helpers/SymSpell/**",
                    "**/Helpers/SymSpellAlgorithm.cs"
                ]
            },
            {
    "condition": "(!AddReadMe)",
                "exclude": [
                    "**/ReadMe.md"
                ]
            }
        ]
    }]
}
```


Part of code that are place depending on "if". The syntax for .cs files is as follows:

```csharp
@*#if (EnableSwaggerSupport)
    app.UseSwagger();
    app.UseSwaggerUI();
    #endif*@
```

The syntax for .csproj files is as follows:

```csproj
  <!--#if (EnableSwaggerSupport) -->
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.3.1" />
    <PackageReference Include="Swashbuckle.AspNetCore.Filters" Version="7.0.3" />
  <!--#endif -->
```

The syntax for .json files is as follows:
```json
  //#if (AddHealthChecks)
  "HealthChecks-UI": {
    "HealthChecks": [
      {
        "Name": "Service One: WebApi",
        "Uri": "/health"
      }
    ],
    "EvaluationTimeInSeconds": 30,
    "MinimumSecondsBetweenFailureNotifications": 60
  },
  //#endif
```

To run the template (from command line)
```cmd
dotnet new MaximalApi --AddSwaggerSupport "true" --AddHealthChecks "false" --AddLinq2dbBulkOperations "true" --AddLazyLoading "false" --AddPolly "true" --AddSerilog "true" --AddSymSpell "true" --AddHttpClient "true"
```

Some custom templates:
- [template-samples](https://github.com/dotnet/dotnet-template-samples)
- [api-template](https://github.com/robbell/dotnet-aks-api-template)

[Other custom template guide](https://github.com/dotnet/templating/wiki/Reference-for-template.json)
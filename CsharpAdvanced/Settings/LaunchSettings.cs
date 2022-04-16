namespace CsharpAdvanced.Settings;

public class LaunchSettings
{
    //LaunchSettings.json is a configuration file on how to launch ASP.NET Core applications.
    //They are important in projects such as web api
    //Remove the profile "iis" because it is obsolete and bring very bad memories!

    //In the LaunchSettigs.json we store some environmental variables, for example:
    /*
    "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
     */
    //That is crucial from the appsettings point of view
    //The default value is "ASPNETCORE_ENVIRONMENT": "Production"

    //Example in Asp.Net project
}

/*
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "ASP.NETCoreWebAPI": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "https://localhost:7240;http://localhost:5240",
      "dotnetRunMessages": true
    },
    "Docker": {
      "commandName": "Docker",
      "launchBrowser": true,
      "launchUrl": "{Scheme}://{ServiceHost}:{ServicePort}/swagger",
      "publishAllPorts": true,
      "useSSL": true
    }
  }
}
*/
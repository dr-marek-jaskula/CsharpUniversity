using Microsoft.Extensions.Configuration;

namespace CsharpAdvanced.Settings;

public class Appsettings
{
    //appsettings are other important program settings that need to be stored in separate file in json format (however it is not a good idea to store there sensitive data)
    //especially it is important for asp.net.core projects and data base projects

    //In one project file we can distinguish the appsettings for the "development" process and "production" version:
    //"appsettings.Development.json" for development stage (they are overwritten by the production version if it is not set otherwise!)
    //"appsettings.json" for release version (they overwrites all other appsettings if it is not set otherwise!)
    //To set the Development .json file as primary we specify it in the "launchSettings.json" file (go to LaunchSettings.cs to cover this topic)
    /* This specifies to honor the "appsettings.Development.json" file
      environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
     */
    //The default ASPNETCORE_ENVIRONMENT is "Production"

    //1) First of all we need to add a NugGet package:
    //Microsoft.Extensions.Configuration.Json
    //2) Add: using Microsoft.Extensions.Configuration; 
    //3) Add "appsettings.json" file (we can also add "appsettings.Development.json") (its JavaScript JSON Configuration File)
    
    //The example file was added, however for more info go to database project (Entity Framework Core) or web api project (Asp.Net.Core)

    public static void InvokeAppsettingsExamples()
    {
        //We add to appsettings some custom data for academic purpose

        //at first we add and build the appsettings and store the result in variable (we can do it manually) - it is needed for the console application.
        //In application like ASP.NET apps (for instance ASP.NET Web API) it is done be default and it does not require any additional code
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile(@"C:\Users\Marek\source\repos\EltinCreator\CsharpUniversity\CsharpAdvanced\appsettings.json").Build();

        //Then we can access the appsettings in the following manner:
        string mySetting = configuration["MySetting"]; //if there is no such setting, returns null
        string subSetting = configuration["MainSetting:SubSetting"]; //the way to get sub objects
        string connectionString = configuration.GetConnectionString("DefaultConnection"); //if there is no value it become empty string
    }
}

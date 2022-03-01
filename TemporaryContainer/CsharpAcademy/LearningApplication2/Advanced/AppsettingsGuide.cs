using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningApplication2.Advanced
{
    class AppsettingsGuide
    {
        //appsettings should go to source control (git, github), but it is not good if u put there sensitive data
        //appsettins from Development (if the development mode is selected (in launchSettings.json, for example (here we dont have them, but in for instance ASP.NET we have them) 
        //if in launchSettings.json there is no "ASPNETCORE_ENVIRONMENT" it is by default set to "Production"
        //so u can made appsettings.Production.json

        public static void LearnAppsettings()
        {
            Console.WriteLine("Hello!");
            //at first we add and build the appsettings and store the result in variable
            //it is needed for the console application. In application like ASP.NET apps (for instance ASP.NET Web API) it is done be default and it does not require any additional code
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            string mySetting = configuration["MySetting"];
            string subSetting = configuration["MainSetting:SubSetting"]; //the way to get sub objects in c#
            
            Console.WriteLine(mySetting);
            Console.WriteLine(subSetting);

            string connectionString = configuration.GetConnectionString("Default"); //if there is no value it become empty string
            Console.WriteLine(connectionString);
            //u want to keep some info separate, but not specifically secret (mb like connection string)
        }

        public static void LearnUserSecrets()
        {
            //1. in order to make user secrets file we first need to install NuGet Package
            //"Microsoft.Extensions.Configuration.UserSecrets"
            //2. then u need to right click on our project and select 
            //"Manage User Secrets" (there is no other option)
            //the file "secrets.json" will be open but it will not be included to the project!! Why? Because it is a secret and it is stored in our computer only! (its good for storing path to our db or others in secrets coz different machines require different paths)

            //secrets are specified for certain app

            //user secrets overrides both appsettings and appsettings.Development (its biggest in hierarchy)

            //however to implement the secrets.json override appsettings.json in console application is too complicated or time eating, so just use it in ASP.NET Core, where it is already done for us

            //in secrets.json u keep local environment (but its is not encrypted - don't try to encrypt it, coz it is not the point, it is only accessible for u, its protected for Windows like other file. However don't keep here sensitive data

        }
    }
}

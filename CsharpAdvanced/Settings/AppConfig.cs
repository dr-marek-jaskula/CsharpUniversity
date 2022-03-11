using System.Configuration;

namespace CsharpAdvanced.Settings;

public class AppConfig
{
    //To add App.config just: add new item -> select Application Configuration File
    //Then install NuGet package:
    //System.Configuration.ConfigurationManager

    //At its simplest, the app.config is an XML file with many predefined configuration sections available and support for custom configuration sections.
    //A "configuration section" is a snippet of XML with a schema meant to store some type of information.
    //Settings can be configured using built-in configuration sections such as connectionStrings or appSettings. You can add your own custom configuration sections

    public static void InvokeAppConfigExamples()
    {
        //Something is strange with this
        var configFile = ConfigurationManager.OpenExeConfiguration(@"C:\Users\Marek\source\repos\EltinCreator\CsharpUniversity\CsharpAdvanced\App.config");
        var settings = configFile.AppSettings.Settings;

        var name = settings["name"];
        var surname = settings["surname"];
        var birthDate = settings["birthDate"];

        if (settings["height"] == null)
            settings.Add("height", "23");
        else
            settings["height"].Value = "32";

        configFile.Save(ConfigurationSaveMode.Modified);
        //ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

        var connectionString = ConfigurationManager.ConnectionStrings["MyKey"];
    }

}


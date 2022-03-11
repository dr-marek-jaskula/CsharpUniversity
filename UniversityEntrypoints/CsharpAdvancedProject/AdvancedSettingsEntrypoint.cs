using CsharpAdvanced.Settings;
using Xunit;

namespace UniversityEntrypoints.CsharpAdvancedProject;

public class AdvancedSettingsEntrypoint
{
    [Fact]
    public void AppsettingsEntrypoint()
    {
        Appsettings.InvokeAppsettingsExamples();
    }
    
    [Fact]
    public void AppConfigEntrypoint()
    {
        AppConfig.InvokeAppConfigExamples();
    }
}


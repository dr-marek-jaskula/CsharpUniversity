namespace CsharpAdvanced.Settings;

public class Secrets
{
    //1) In order to make user secrets file we first need to install NuGet package:
    //"Microsoft.Extensions.Configuration.UserSecrets"
    //2. Then u need to right click on our project and select 
    //"Manage User Secrets" (there is no other option)
    //File "secrets.json" will be open but it will not be included to the project!!
    //Why? Because it is a secret and it is stored in our computer only!
    //(its good for storing path to our db or others in secrets because different machines require different paths)

    //secrets are specified for certain app

    //user secrets overrides both appsettings and appsettings.Development (its biggest in hierarchy)

    //However to implement the secrets.json override appsettings.json in console application is too complicated or time eating
    //Therefore, just use it in ASP.NET Core, where it is already done for us

    //in secrets.json u keep local environment
    //However data is not encrypted - don't try to encrypt it, because it is not the point, it is only accessible for u, its protected for Windows like other file.
    //Nevertheless, don't keep here sensitive data - it is not to store sensitive data!
}


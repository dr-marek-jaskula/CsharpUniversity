using LinqToDB.Common;
using Microsoft.Extensions.Options;

namespace ASP.NETCoreWebAPI.Options;

//To apply binding and mapping we implement the IConfigureOptions
public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
{
    //We inject the  IConfiguration 
    private readonly IConfiguration _configuration;

    //It is good practice to store the configuration section name in a constant field:
    private readonly string _configurationSectionName = "DatabaseOptions";

    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        options.ConnectionString = _configuration.GetConnectionString("DefaultConnection");

        //We bind the section in a appsettings.json with the DatabaseOptions instance
        _configuration.GetSection(_configurationSectionName).Bind(options);
    }
}

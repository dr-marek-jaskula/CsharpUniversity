namespace ASP.NETCoreWebAPI.Authentication;

//Here we use the widely use approach for Authentication using "Jwt"

public class AuthenticationSettings
{
    //Determines the private key
    public string JwtKey { get; set; } = string.Empty;

    //Determines for how long the authentication holds
    public int JwtExpireDays { get; set; }

    //Determines the Issuer of WebApi
    public string JwtIssuer { get; set; } = string.Empty;
}

//Next in the appsettings.json we need to specify our settings (we can also use secrets)

/*
  "Authentication": {
    "JwtKey": "PRIVATE_KEY_DONT_SHARE",
    "JwtExpireDayes": 15,
    "JwtIssuer": "http://CsharpUniversityWebApi.com"
  }
*/

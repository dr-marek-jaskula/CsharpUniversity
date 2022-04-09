using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ASP.NETCoreWebAPI.Authentication;
using ASP.NETCoreWebAPI.DatabaseEntitiesWebApi;
using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using AutoMapper;
using EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ASP.NETCoreWebAPI.Services;

public interface IAccountService
{
    void RegisterUser(RegisterUserDto dto);
    string GenerateJwt(LoginDto dto);
}

public class AccountService : IAccountService
{
    private readonly MyDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;

    public AccountService(MyDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
    }

    public void RegisterUser(RegisterUserDto dto)
    {
        var newUser = new User()
        {
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            Nationality = dto.Nationality,
            RoleId = dto.RoleId
        };

        string hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
        newUser.PasswordHash = hashedPassword;

        _context.Users.Add(newUser);
        _context.SaveChanges();
    }

    public string GenerateJwt(LoginDto dto)
    {
        var user = _context.Users
            .Include(u=>u.Role)
            .FirstOrDefault(u => u.Email == dto.Email);

        if (user is null) 
            throw new BadRequestException("Invalid username or password");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if (result is PasswordVerificationResult.Failed)
            throw new BadRequestException("Invalid username or password");

        //The list of claims
        var claims = new List<Claim>()
        {
            //Claims base types:
            new Claim(type: ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            //Claims custom types
            new Claim(type: "DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
        };
        
        //Additional claim and authorization based upon it
        if (!string.IsNullOrEmpty(user.Nationality))
            claims.Add(new Claim(type: "Nationality", user.Nationality));

        //Create key variable using appsetting.json
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

        //Generate credentials that are needed for a authorize jwt token
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //Token duration
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        //Token
        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims: claims, expires: expires, signingCredentials: cred);

        //Token handler
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ASP.NETCoreWebAPI.Authentication;
using ASP.NETCoreWebAPI.Exceptions;
using ASP.NETCoreWebAPI.Models.DataTransferObjects;
using AutoMapper;
using EFCore;
using EFCore.Data_models;
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
    private readonly IMapper _mapper;

    public AccountService(MyDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IMapper mapper)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
        _mapper = mapper;
    }

    public void RegisterUser(RegisterUserDto dto)
    {
        //Map from RegisterUserDto to User
        var newUser = _mapper.Map<User>(dto);

        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);

        _context.Users.Add(newUser);
        _context.SaveChanges();
    }

    public string GenerateJwt(LoginDto dto)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Email == dto.Email);

        if (user is null)
            throw new BadRequestException("Invalid email or password");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if (result is PasswordVerificationResult.Failed)
            throw new BadRequestException("Invalid email or password");

        //Explicit loading
        _context.Entry(user)
            .Reference(u => u.Person)
            .Query()
            .Include(p => p.Address)
            .Load();

        _context.Entry(user)
            .Reference(u => u.Role)
            .Query()
            .Load();

        //The list of claims
        var claims = new List<Claim>()
        {
            //Claims base types:
            new Claim(type: ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.Username}"),
            new Claim(ClaimTypes.Role, $"{user.Role?.Name}"),
        };

        //Claims custom types (authorization based upon it)
        claims.Add(new Claim(type: ClaimPolicy.DateOfBirth, user switch
        {
            { Person.DateOfBirth: DateOnly } => user.Person.DateOfBirth.Value.ToString("yyyy-MM-dd"),
            _ => ""
        }));

        //Nationality claim
        claims.Add(new Claim(type: ClaimPolicy.Nationality, user switch
        {
            { Person.Address.Country: string { Length: > 0 } } => user.Person.Address.Country,
            _ => ""
        }));

        //PersonId (EmployeeId or CustomerId) claim to examine CreatedById (for ResourceOperationHandlerRequirement)
        claims.Add(new Claim(type: ClaimPolicy.PersonId, user switch
        {
            { PersonId: null } => $"{user.PersonId}",
            _ => ""
        }));

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
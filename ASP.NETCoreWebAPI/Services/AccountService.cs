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

        string hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
        newUser.PasswordHash = hashedPassword;

        _context.Users.Add(newUser);
        _context.SaveChanges();
    }

    public string GenerateJwt(LoginDto dto)
    {
        var user = _context.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Email == dto.Email);

        if (user is null)
            throw new BadRequestException("Invalid username or password");

        if (user.EmployeeId is int employeeId)
            user.Employee = _context.Employees
                .Include(e => e.Address)
                .FirstOrDefault(e => e.Id == employeeId);
        else if (user.CustomerId is int customerId)
            user.Customer = _context.Customers
                .Include(c => c.Address)
                .FirstOrDefault(c => c.Id == customerId);

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if (result is PasswordVerificationResult.Failed)
            throw new BadRequestException("Invalid username or password");

        //The list of claims
        var claims = new List<Claim>()
        {
            //Claims base types:
            new Claim(type: ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, $"{user.Username}"),
            new Claim(ClaimTypes.Role, $"{user.Role?.Name}"),
        };

        //Claims custom types (authorization based upon it)
        if (user is { Employee.DateOfBirth: DateTime } or { Customer.DateOfBirth: DateTime })
        {
            claims.Add(new Claim(type: ClaimPolicy.DateOfBirth, user switch
            {
                { Employee.DateOfBirth: DateTime } => user.Employee.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                { Customer.DateOfBirth: DateTime } => user.Customer.DateOfBirth.Value.ToString("yyyy-MM-dd"),
                _ => ""
            }));
        }

        //Nationality claim
        if (user is { Employee.Address.Country: string { Length: > 0 } } or { Customer.Address.Country: string { Length: > 0 } })
        {
            claims.Add(new Claim(type: ClaimPolicy.Nationality, user switch
            {
                { Employee.Address.Country: string { Length: > 0 } } => user.Employee.Address.Country,
                { Customer.Address.Country: string { Length: > 0 } } => user.Customer.Address.Country,
                _ => ""
            }));
        }

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
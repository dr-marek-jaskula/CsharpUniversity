using System.Security.Claims;

namespace ASP.NETCoreWebAPI.Services;

public interface IUserContextService
{
    public ClaimsPrincipal? User { get; }
    public int? GetUserId { get; }
}

//This class is responsible for sharing the information about certain user based on the HTTP Context
public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    //Informations about user from Context
    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public int? GetUserId => User is null ? null : int.Parse(User.FindFirst(c => c.Type is ClaimTypes.NameIdentifier).Value);
}

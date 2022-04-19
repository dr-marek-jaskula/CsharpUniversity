using ASP.NETCoreWebAPI.Authentication;
using System.Security.Claims;

namespace ASP.NETCoreWebAPI.Services;

public interface IUserContextService
{
    public ClaimsPrincipal? User { get; }
    public int? GetUserId { get; }
    public int? GetPersonId { get; }
}

//This class is responsible for sharing the information about certain user based on the HTTP Context (so we will be free from strong connection to HttpContext for user data)
//We will be able to get User data in every Service by injecting the IUserContextService
public class UserContextService : IUserContextService
{
    //4. We need to Register Service "AddHttpContextAccessor" to be able to Access the IUserService (by this we can inject "IHttpContextAccessor" into IUserContextService)
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    //Informations about user from Context
    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    //Get information about UserId
    public int? GetUserId => User is null ? null : int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    //Get information about PersonId
    public int? GetPersonId
    {
        get
        {
            if (User?.FindFirstValue(ClaimPolicy.PersonId) is string stringPersonId)
                return int.Parse(stringPersonId);

            return null;
        }
    }
}
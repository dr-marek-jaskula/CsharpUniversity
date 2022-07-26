namespace ASP.NETCoreWebAPI.Authentication;

//ClaimPolicies are my own custom claims
//ClaimTypes are default claims

//General
//My Authorization Policy
public static class MyAuthorizationPolicy
{
    public const string HasNationality = "HasNationality";
    public const string Mature = "Mature";
    public const string CreateAtLeastTwoOrders = "CreateAtLeastTwoOrders";
}

public static class ClaimPolicy
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";
    public const string PersonId = "PersonId";
}

//Requirements

public static class MaturePolicy
{
    public const int Eighteen = 18;
}

public static class CreateAtLeast
{
    public const int Two = 2;
}

//Claims

public static class ClaimHasNationality
{
    public const string Poland = "Poland";
    public const string Germany = "Germany";
    public const string Valheim = "Valheim";
}

public static class ClaimRoles
{
    public const string Manager = "Manager";
    public const string Administrator = "Administrator";
    public const string Employee = "Employee";
    public const string Customer = "Customer";
}
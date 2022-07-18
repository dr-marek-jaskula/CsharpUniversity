using ValueObjects.Common;

namespace ValueObjects;

//FluentValidation.AspNetCore and ValueOf NuGet Packages are required
//This is how to create fast ValueObjects using ValueOf NuGet Package

public class CustomerWithGitHub
{
    public CustomerWithGitHubId Id { get; init; } = CustomerWithGitHubId.From(Guid.NewGuid());

    public GitHubUsername GitHubUsername { get; init; } = default!;

    public FullName FullName { get; init; } = default!;

    public Email Email { get; init; } = default!;

    public DateOfBirth DateOfBirth { get; init; } = default!;
}

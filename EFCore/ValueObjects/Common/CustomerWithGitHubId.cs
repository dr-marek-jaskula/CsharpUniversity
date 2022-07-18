using ValueOf;

namespace ValueObjects.Common;

public class CustomerWithGitHubId : ValueOf<Guid, CustomerWithGitHubId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException("Customer Id cannot be empty", nameof(CustomerWithGitHubId));
        }
    }
}

using Xunit;
using EFCore.Bogus;
using EFCore;

namespace UniversityEntrypoints.EFCore;

public class EFCoreEntrypoint
{

    [Fact]
    public void EFCoreGenerateEntrypoint()
    {
        MyDbContextFactory myDbContextFactory = new();
        MyDbContext myDbContext = myDbContextFactory.CreateDbContext();

        DemoDataGenerator demoDataGenerator = new(myDbContext);
        demoDataGenerator.Generate();
    }
}


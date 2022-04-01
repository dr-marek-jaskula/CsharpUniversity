using Xunit;
using EFCore.BogusDemo;
using EFCore;

namespace UniversityEntrypoints.EFCore;

public class EFCoreDatabaseEntrypoint
{

    [Fact]
    public void EFCoreGenerateEntrypoint()
    {
        MyDbContextFactory myDbContextFactory = new();
        MyDbContext myDbContext = myDbContextFactory.CreateDbContext();

        DemoDataGenerator demoDataGenerator = new(myDbContext);
        demoDataGenerator.Generate();
    }

    [Fact]
    public void EFCoreClearEntrypoint()
    {
        MyDbContextFactory myDbContextFactory = new();
        MyDbContext myDbContext = myDbContextFactory.CreateDbContext();

        DemoDataGenerator demoDataGenerator = new(myDbContext);
        demoDataGenerator.ClearDatabase();
    }
}


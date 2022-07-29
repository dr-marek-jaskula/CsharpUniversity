using Xunit;
using EFCore.BogusDemo;
using EFCore;

namespace UniversityEntrypoints.EFCore;

public class EFCoreDatabaseEntrypoint
{
    [Fact]
    public void EFCoreSeedDatabaseEntrypoint()
    {
        MyDbContextFactory myDbContextFactory = new();
        MyDbContext myDbContext = myDbContextFactory.CreateDbContext();

        DemoDataGenerator demoDataGenerator = new(myDbContext);
        demoDataGenerator.SeedDatabase();
    }

    [Fact]
    public void EFCoreClearDatabaseEntrypoint()
    {
        MyDbContextFactory myDbContextFactory = new();
        MyDbContext myDbContext = myDbContextFactory.CreateDbContext();

        DemoDataGenerator demoDataGenerator = new(myDbContext);
        //demoDataGenerator.ClearDatabase();
    }
}
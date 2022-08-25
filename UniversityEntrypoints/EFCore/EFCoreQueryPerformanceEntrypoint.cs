using Xunit;
using EFCore;
using EFCore.EF_Core_advance;
using System.Threading.Tasks;

namespace UniversityEntrypoints.EFCore;

public class EFCoreQueryPerformanceEntrypoint
{

    [Fact]
    public void EFCoreTrackingQueryProductEntrypoint()
    {
        MyDbContextFactory myDbContextFactory = new();
        MyDbContext myDbContext = myDbContextFactory.CreateDbContext();

        Tracking tracking = new(myDbContext);
        tracking.QueryProduct();
    }

    [Fact]
    public void EFCoreTrackingQueryProductAsNoTrackingEntrypoint()
    {
        MyDbContextFactory myDbContextFactory = new();
        MyDbContext myDbContext = myDbContextFactory.CreateDbContext();

        Tracking tracking = new(myDbContext);
        tracking.QueryProductAsNoTracking();
    }

    [Fact]
    public void EFCoreFourthPrincipleExampleEntrypoint()
    {
        MyDbContextFactory myDbContextFactory = new();
        MyDbContext myDbContext = myDbContextFactory.CreateDbContext();

        QueryPerformance queryPerformance = new(myDbContext);
        queryPerformance.FourthPrincipleExample();
    }

    [Fact]
    public async Task EFCoreBulkInsertExampleEntrypoint()
    {
        MyDbContextFactory myDbContextFactory = new();
        MyDbContext myDbContext = myDbContextFactory.CreateDbContext();

        BulkInsert bulkInsert = new(myDbContext);
        await bulkInsert.BulkInsertExample();
    }
}


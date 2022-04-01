using Xunit;
using EFCore;
using EFCore.EF_Core_advance;

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
}


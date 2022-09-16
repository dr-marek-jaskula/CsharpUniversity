using EFCore;
using ASP.NETCoreWebAPI.Options;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class DatabaseContextRegistration
{
    public static void RegisterDatabaseContext(this IServiceCollection services, bool isDevelopment)
    {
        //The default way is to use "AddDbContext".
        //However, then for each request a new database context will be created. For performance reasons we can use context pooling, so the context will be reused
        //The only problem is when the context maintains the state (for instance a private field) - nonetheless it is rare situation, so rather use "AddDbContextPool"
        services.AddDbContextPool<MyDbContext>((serviceProvider, optionsBuilder) =>
        {
            //It is important to get the value of "IOptions" of "DatabaseOptions" (OptionPattern)
            var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            optionsBuilder
                //.UseLazyLoadingProxies() //To configure all queries to LazyLoading (be careful of it, LazyLoading can cause troubles)
                .UseSqlServer(databaseOptions.ConnectionString, options =>
                {
                    options.CommandTimeout(databaseOptions.CommandTimeout);
                    //We can add some error numbers if we want to. Otherwise, leave Array.Empty<int>(). Retries are very important
                    options.EnableRetryOnFailure(databaseOptions.MaxRetryCount, TimeSpan.FromSeconds(databaseOptions.MaxRetryDelay), Array.Empty<int>());

                    //To force the single inserts (not insert with merges) - so when we insert multiple records we have one big statement, which is good for inserts. But in some rare cases we want multiple single ones
                    //options.MaxBatchSize(1);
                });

            //We can also set "not tracking" as default behavior
            //optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            //Log additional information in development stage to deal with problems
            if (isDevelopment)
            {
                optionsBuilder.EnableDetailedErrors(); //To get field-level error details
                //DO NOT USE "EnableSensitiveDataLogging" IN PRODUCTION!
                optionsBuilder.EnableSensitiveDataLogging(); //DO NOT USE THIS IN PRODUCTIN! Used to get parameter values. DO NOT USE THIS IN PRODUCTIN!
                optionsBuilder.ConfigureWarnings(warningAction =>
                {
                    warningAction.Log(new EventId[]
                    {
                        CoreEventId.FirstWithoutOrderByAndFilterWarning,
                        CoreEventId.RowLimitingOperationWithoutOrderByWarning
                    });
                });
            }
        });
        //Both with UseDeveloperExceptionPage provides default exception handling for "Developer" stage of api. More information below near "UseDeveloperExceptionPage"
        //.AddDatabaseDeveloperPageExceptionFilter();
    }
}

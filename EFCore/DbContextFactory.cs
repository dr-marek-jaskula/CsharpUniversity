using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EFCore;

#region Factory for MS Sql Server

public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
{
    public MyDbContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new MyDbContext(optionsBuilder.Options);
    }
}

#endregion

#region Factory for MySql

//public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
//{
//    public MyDbContext CreateDbContext(string[]? args = null)
//    {
//        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

//        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
//        optionsBuilder.UseMySQL(configuration["ConnectionStrings:DefaultConnection"]);

//        return new MyDbContext(optionsBuilder.Options);
//    }
//}

/* //PomeloVersion
public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
{
    public MyDbContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 27)); //needed for Pomelo MySql

        var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
        optionsBuilder.UseMySQL(configuration["ConnectionStrings:DefaultConnection"], serverVersion);

        return new MyDbContext(optionsBuilder.Options);
    }
}											
*/

#endregion
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EFCore;

//We need to create a custom DbContext (here named "MyDbContext") that inherits from the DbContext class
//Using this class we will connect manipulate the database entities by manipulating class properties (DbSet)

public class MyDbContext : DbContext
{
    //1. In this class we need to write the following constructor
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    //2. DbSets should be determine here
    
    /*
    public DbSet<User> Users => Set<User>();
    public DbSet<UserGroup> UserGroups => Set<UserGroup>();
    public DbSet<Identity> Identities => Set<Identity>();
    */
    
    //Each class (like User) should be previously defined (and configured. I prefer configuration using IEntityTypeConfiguration)

    //Other example
    /*
    public DbSet<IndependentBonusEntity> IndependentBonuses => Set<IndependentBonusEntity>();
    public DbSet<DependentBonusEntity> DependentBonuses => Set<DependentBonusEntity>();
    public DbSet<CharacterEntity> Characters => Set<CharacterEntity>();
    public DbSet<FeatureEntity> Features => Set<FeatureEntity>();
    public DbSet<ManeuverEntity> Maneuvers => Set<ManeuverEntity>();
    public DbSet<StanceEntity> Stances => Set<StanceEntity>();
    public DbSet<StateEntity> States => Set<StateEntity>();
    public DbSet<ArmorEntity> Armors => Set<ArmorEntity>();
    */

    //3. The OnModelCreating method should be overridden
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //4 My preferred way is to get configuration from the Assembly (from each file separately). Nevertheless, it can be done all here
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

//Sometimes the factory is placed in the same file as DbContext
//For educational purpose we will store them separately
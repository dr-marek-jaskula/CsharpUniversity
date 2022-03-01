using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ASPDotNetLearningApplication
{
    //dziedziczenie klasy z Entity Framework

    public class RestaurantDbContext : DbContext
    {
        //connection string to pliku appsetting.json (albo development). Tym celu trzeba te� zmieni� rejestracj� klasy RestaurantDbContext aby widzia� connection stringa
        //private string _connectionString = "Server=DESKTOP-QSPT4JM;Database=RestaurantDb;Trusted_Connection=True;";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        //nadpisanie metody daj�c now� konfiguracj�, tzn zeby kolumna by�a wymagana i okre�li� d�ugo��
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().Property(r => r.Name).IsRequired().HasMaxLength(25);

            modelBuilder.Entity<Dish>().Property(d => d.Name).IsRequired();

            modelBuilder.Entity<Address>().Property(a => a.City).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Address>().Property(a => a.Street).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();

            modelBuilder.Entity<Role>().Property(u => u.Name).IsRequired();
        }

        //ten konstruktor jest pod konfiguracj� z connection stringiem, czyli robi to co ta funkcja OnConfiguring, kt�ra zakomentwali�my
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }

        //to wywalamy bo robimy to ju� w Startupie
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //    //teraz robimy migracje, kt�ra wskarze EntityFramework wszystkie kroki kt�re s� niezb�dne aby odwzorowa� t� klas�w bazie danych
        //}
        
    }
}

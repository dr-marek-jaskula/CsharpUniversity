using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ASPDotNetLearningApplication
{
    //to servis ktory słuzy do tego, aby seedowac bazę danych - to znacyz upewniać sie ze kluczowe dane są bazie danych, np ze MUSZĄ byc konkretne restauracje w bazie.Nie jest do regularnego uzupełniania bazy danych ale można
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContex;

        public RestaurantSeeder(RestaurantDbContext dbContex)
        {
            _dbContex = dbContex;
        }

        public void Seed()
        {
            if (_dbContex.Database.CanConnect())
            {
                //automatyczna migracja baz danych
                //sprawdzamy wszystkei migracje ktore nie zostaly zaaplikowane
                var pendingMigrations = _dbContex.Database.GetPendingMigrations();

                //sprawdzamy ze nie jest nullem i ma jakąś wartość. Czyli jest migracja do dodania
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    //dzięki temu migracje których nie było zostaną zaaplikowane
                    _dbContex.Database.Migrate();
                }

                if (!_dbContex.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContex.Roles.AddRange(roles);
                    _dbContex.SaveChanges();
                }

                if (!_dbContex.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbContex.Restaurants.AddRange(restaurants);
                    _dbContex.SaveChanges();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Nashille Hot Chicken", Price = 10.30M },
                        new Dish() { Name = "Cichken Nuggets", Price = 5.30M, }
                    },
                    Address = new Address()
                    {
                        City = "Krawków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonald",
                    Category = "Fast Food2",
                    Description = "So-called 'Mac' is the worst possible way to feed ourselfs",
                    ContactEmail = "contact@mcDonald.com",
                    HasDelivery = false,
                    Dishes = new List<Dish>()
                    {
                        new Dish() { Name = "Fish with no fish", Price = 15.30M },
                        new Dish() { Name = "Old Nuggets", Price = 11.30M, }
                    },
                    Address = new Address()
                    {
                        City = "Warsow",
                        Street = "Moja 8",
                        PostalCode = "31-201"
                    }
                },
            };
            return restaurants;
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }

    }
}
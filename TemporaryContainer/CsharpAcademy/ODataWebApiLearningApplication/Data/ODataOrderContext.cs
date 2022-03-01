using Microsoft.EntityFrameworkCore;
using System;

namespace ODataWebApiLearningApplication
{
    public class ODataOrderContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ODataOrderContext(DbContextOptions<ODataOrderContext> context) : base(context)
        {

        }
    }
}

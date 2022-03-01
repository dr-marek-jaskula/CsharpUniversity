using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODataWebApiLearningApplication
{

    public class CustomerController : ODataController
    {
        private readonly ODataOrderContext _context;

        public CustomerController(ODataOrderContext context)
        {
            _context = context;
}

        [EnableQuery(PageSize = 5)] //with pagination
        // [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy)] //optional to specify what is allowed (here not in startup)
        public IActionResult Get()
        {
            return Ok(_context.Customers);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }


        private readonly List<string> _demoCustomers = new()
        {
            "Foo",
            "Bar",
            "Acme",
            "King of Tech",
            "Awesomeness"
        };

        private readonly List<string> _demoProducts = new()
        {
            "Bike",
            "Car",
            "Apple",
            "Spaceship"
        };

        private readonly List<string> _demoCountries = new()
        {
            "AT",
            "DE",
            "CH"
        };

        [HttpPost]
        [Route("fill")]
        public async Task<IActionResult> Fill()
        {
            var rand = new Random();
            for (var i = 0; i < 10; i++)
            {
                var c = new Customer
                {
                    CustomerName = _demoCustomers[rand.Next(_demoCustomers.Count)],
                    CountryId = _demoCountries[rand.Next(_demoCountries.Count)]
                };
                _context.Customers.Add(c);

                for (var j = 0; j < 10; j++)
                {
                    var o = new Order
                    {
                        OrderDate = DateTime.Today,
                        Product = _demoProducts[rand.Next(_demoProducts.Count)],
                        Quantity = rand.Next(1, 5),
                        Revenue = rand.Next(100, 5000),
                        Customer = c
                    };
                    _context.Orders.Add(o);
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

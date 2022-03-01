using System;
using System.Collections.Generic;

namespace ODataWebApiLearningApplication
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CountryId { get; set; }
        public List<Order> Orders { get; set; }
    }
}

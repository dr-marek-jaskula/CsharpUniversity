using System;

namespace ODataWebApiLearningApplication
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public int Revenue { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}

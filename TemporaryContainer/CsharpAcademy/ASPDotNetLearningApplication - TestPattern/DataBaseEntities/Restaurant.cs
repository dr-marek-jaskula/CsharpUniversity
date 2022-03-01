using System;
using System.Collections.Generic;

namespace ASPDotNetLearningApplication
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }

        public int? CreatedById { get; set; }
        public virtual User CreateBy { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Dish> Dishes { get; set; }

    }
}

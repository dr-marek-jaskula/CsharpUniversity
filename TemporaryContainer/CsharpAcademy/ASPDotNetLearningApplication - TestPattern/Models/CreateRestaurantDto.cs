using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPDotNetLearningApplication
{
    /// <summary>
    /// Its a class for clients with data validation (using attributes)
    /// </summary>
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}

/*
 {
    "name": "Manager restaurant",
    "description": "desc1",
    "category": "italian",
    "hasDelivery": false,
    "contactEmail": "contme@pizza.com",
    "City": "Krakow",
    "Street": "Saska 15",
    "PostalCode": "31-114"
} 
 */

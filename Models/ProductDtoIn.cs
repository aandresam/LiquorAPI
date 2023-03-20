using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace liquorApi.Models
{
    public class ProductDtoIn
    {

        [Required(ErrorMessage = "The name of the product is required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "The description of the product is required")]
        public string Description { get; set; } = null!;

        [
            Required(ErrorMessage = "The price is required"),
            Range(0, int.MaxValue, ErrorMessage = "The price cannot be less than 0")
        ]
        public decimal Price { get; set; }

        [
            Required(ErrorMessage = "The stock is required"),
            Range(0, int.MaxValue, ErrorMessage = "The stock cannot be less than 0")
        ]
        public int Stock { get; set; }
    }
}
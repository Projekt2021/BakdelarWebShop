using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Classes
{
    public class ProductView
    {
        public int ProductId { get; set; }
        
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Description")]
        public string ProductDescription { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }

        //[Required]
        [Display(Name = "Available Quantity")]
        public int? AvailableQuantity { get; set; }

        //[Required]
        [Display(Name = "Weight")]
        public double? ProductWeight { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        public CategoryView Category { get; set; }

        public List<ProductImageView> ProductImageView { get; set; }
    }
}

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

<<<<<<< Updated upstream
        public DateTime DateEntered { get; set; }
        public double? SpecialPrice { get; set; }
        public bool IsSelected { get; set; }
        public int NumberOfSold { get; set; }
=======
        [Display(Name = "Created At")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateEntered { get; set; } //Remove this property and use Datetime.Now in controller?

        [Display(Name = "Latest")]
        public bool IsSelected { get; set; }

        [Display(Name = "Sold Count")]
        public int NumberOfSold { get; set; } //Remove?
>>>>>>> Stashed changes



        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        public CategoryView Category { get; set; }

        public List<ProductImageView> ProductImageView { get; set; }
    }
}

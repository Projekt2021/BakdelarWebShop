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
        //[DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = false)]
        public decimal ProductPrice { get; set; }

        [Display(Name="Special Prize")]
        public decimal? SpecialPrice { get; set; }

        [Display(Name = "Available Quantity")]
        public int? AvailableQuantity { get; set; }

        [Display(Name = "Weight")]
        public double? ProductWeight { get; set; }

        [Display(Name = "Selected")]
        public bool IsSelected { get; set; }

        [Display(Name = "Sold Count")]
        public int NumberOfSold { get; set; }

        [Display(Name = "Date Entered")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateEntered { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        public CategoryView Category { get; set; }

        public List<ProductImageView> ProductImageView { get; set; }

    }

}

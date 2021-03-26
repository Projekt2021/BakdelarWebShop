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
        [Display(Name = "Produktnamn")]
        public string ProductName { get; set; }

        [Display(Name = "Beskrivning")]
        public string ProductDescription { get; set; }
        
        [Required]
        [Display(Name = "Pris")]
        [RegularExpression(@"^[0-9]+(,[0-9]{1,2})$", ErrorMessage = "Tal med två decimaler, tack. Ditt fån.")]
        public decimal ProductPrice { get; set; }

        [Display(Name="Nedsatt pris, yo")]
        public decimal? SpecialPrice { get; set; }

        [Display(Name = "I lager")]
        public int? AvailableQuantity { get; set; }

        [Display(Name = "Vikt")]
        public double? ProductWeight { get; set; }

        [Display(Name = "Kampanj")]
        public bool IsSelected { get; set; }

        [Display(Name = "Antal sålda")]
        public int NumberOfSold { get; set; }

        [Display(Name = "Tillagd datum")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateEntered { get; set; }

        [Required]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        
        public CategoryView Category { get; set; }

        public List<ProductImageView> ProductImageView { get; set; }

    }

}

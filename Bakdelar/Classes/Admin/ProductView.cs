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
        [RegularExpression(@"^\d{1,3}(,\d{0,2})?$", ErrorMessage = "Decimaltal upp till 999,99, tack. Ditt fån.")]
        public decimal ProductPrice { get; set; }

        [Display(Name="Reapris")]
        [RegularExpression(@"^\d{1,3}(,\d{0,2})?$", ErrorMessage = "Decimaltal upp till 999,99, tack.")]
        public decimal? SpecialPrice { get; set; }
        
        [Display(Name = "Vikt")]
        [RegularExpression(@"^\d{1,3}(,\d{0,2})?$", ErrorMessage = "Decimaltal upp till 999,99, tack.")]
        public double? ProductWeight { get; set; }

        [Display(Name = "I lager")]
        [RegularExpression(@"^\d{0,4}$", ErrorMessage = "Ange ett heltal, tack.")]
        public int? AvailableQuantity { get; set; }

        [Display(Name = "Kampanjvara")]
        public bool IsSelected { get; set; }

        [Display(Name = "Antal sålda")]
        public int NumberOfSold { get; set; }

        [Display(Name = "Tillagd datum")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateEntered { get; set; }

        public int CategoryId { get; set; }
        
        [Display(Name = "Kategori")]
        public CategoryView Category { get; set; }

        [Display(Name = "Bildgalleri")]
        public List<ProductImageView> ProductImageView { get; set; }

    }

}

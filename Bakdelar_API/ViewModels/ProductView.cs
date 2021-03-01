using DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Bakdelar_API.ViewModels
{
    public class ProductView
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }

        public int? AvailableQuantity { get; set; }

        public double? ProductWeight { get; set; }
        public DateTime DateEntered { get; set; }
        public double? SpecialPrice { get; set; }
        public bool IsSelected { get; set; }
        public int NumberOfSold { get; set; }

        public int CategoryId { get; set; }

        public CategoryView Category { get; set; }

        public List<ProductImageView> ProductImageView { get; set; }
       
    }
}

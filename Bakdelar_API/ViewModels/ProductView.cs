using DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar_API.ViewModels
{
    public class ProductView
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        public decimal? SpecialPrice { get; set; }
        
        public int? AvailableQuantity { get; set; }

        public double? ProductWeight { get; set; }
        public DateTime DateEntered { get; set; }
        public bool IsSelected { get; set; }
        public int NumberOfSold { get; set; }

        public int CategoryId { get; set; }

        public CategoryView Category { get; set; }

        public List<ProductImageView> ProductImageView { get; set; }

        public ProductView()
        {

        }

        public ProductView(Product p)
        {
            ProductId = p.ProductId;
            ProductName = p.ProductName;
            ProductDescription = p.ProductDescription;
            ProductPrice = p.ProductPrice;
            SpecialPrice = p.SpecialPrice;
            AvailableQuantity = p.AvailableQuantity;
            ProductWeight = p.ProductWeight;
            DateEntered = p.DateEntered;
            DateEntered = p.DateEntered;
            IsSelected = p.IsSelected;
            NumberOfSold = p.NumberOfSold;
            //Cascade insert
            Category = new CategoryView
            {
                CategoryId = p.Category.CategoryId,
                CategoryName = p.Category.CategoryName
            };
            ProductImageView = p.ProductImages.Select(x => new ProductImageView { ImageId = x.ImageId, ImageURL = x.ImageURL }).ToList();
        }
    }
}
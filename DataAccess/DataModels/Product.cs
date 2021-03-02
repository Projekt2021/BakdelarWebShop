using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DataModels
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required, MaxLength(100), StringLength(100)]
        public string ProductName { get; set; }

        [Required, MaxLength(500), StringLength(500)]
        public string ProductDescription { get; set; }

        [Required, Column(TypeName = "decimal(5,2)")]
        public decimal ProductPrice { get; set; }

        public int? AvailableQuantity { get; set; }

        public double? ProductWeight { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? SpecialPrice { get; set; }

        public bool IsSelected { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateEntered { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

        public Product() { }

        public Product(string productName, string productDescription, decimal productPrice, int? availableQuantity, int? productWeight, decimal? specialPrice, bool isSelected, DateTime dateEntered,
                        int categoryID, ICollection<ProductImage> productImages) 
        {
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            AvailableQuantity = availableQuantity;
            ProductWeight = productWeight;
            SpecialPrice = specialPrice;
            IsSelected = isSelected;
            DateEntered = dateEntered;
            CategoryId = categoryID;
            ProductImages = productImages;
        }
    }
}

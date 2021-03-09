using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Classes
{

    public class ShoppingBasketItem
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Produkt")]
        public string ProductName { get; set; }


        [Display(Name = "Pris")]
        public decimal Price { get; set; }

        [Display(Name = "Antal")]
        public int ItemCount { get; set; }

        [Display(Name = "Bild")]
        public string ImageLink { get; set; }

        [Display(Name = "Rabatt")]
        public bool Discount { get; set; }
        public int StockLevel { get; set; }


        public override bool Equals(object obj)
        {
            var other = obj as ShoppingBasketItem;
            return this.ID == other.ID
                && this.ProductName == other.ProductName
                && this.Price == other.Price
                && this.ItemCount == other.ItemCount
                && this.ImageLink == other.ImageLink
                && this.Discount == other.Discount
                && this.StockLevel == other.StockLevel;
        }
    }
}

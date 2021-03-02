using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Classes
{
    public class ShoppingBasket
    {
        public List<ShoppingBasketItem> Items { get; set; } 
        
    }

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

        [Display(Name = "Rabatt")]
        public bool Discount { get; set; }
    }
}

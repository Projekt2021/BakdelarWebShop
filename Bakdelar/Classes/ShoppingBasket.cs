using System;
using System.Collections.Generic;
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
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
    }
}

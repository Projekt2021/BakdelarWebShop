using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bakdelar.Models
{
    public class OrderItem
    {
        public OrderItem()
        {

        }


        public OrderItem(Classes.ShoppingBasketItem shoppingBasketItem)
        {
            ProductID = shoppingBasketItem.ID;
            AmountOrdered = shoppingBasketItem.ItemCount;
            ProductPricePaidEach = shoppingBasketItem.Price;
            ProductPricePaidTotal = shoppingBasketItem.Price*shoppingBasketItem.ItemCount;
            ProductName = shoppingBasketItem.ProductName;

        }




        public int OrderItemID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPricePaidEach { get; set; }



        public decimal ProductPricePaidTotal { get; set; }
        public int AmountOrdered { get; set; }
    }
}
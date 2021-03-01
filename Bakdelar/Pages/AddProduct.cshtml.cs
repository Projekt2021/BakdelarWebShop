using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakdelar.Pages
{
    public class AddProductModel : PageModel
    {
        public void OnGet()
        {
        }

        //public void OnPost()
        //{
        //    var cookie = Request.Cookies["shopping_basket"];
        //    if (cookie != null)
        //    {
        //        var shoppingBasket = JsonSerializer.Deserialize<Classes.ShoppingBasket>(cookie);
        //        shoppingBasket.Items.Add(ShoppingItem);
        //        Response.Cookies.Delete("shopping_basket");
        //        Response.Cookies.Append("shopping_basket", JsonSerializer.Serialize(shoppingBasket));
        //    }
        //    else
        //    {
        //        var shoppingBasket = new Classes.ShoppingBasket();
        //        shoppingBasket.Items.Add(ShoppingItem);
        //        Response.Cookies.Append("shopping_basket", JsonSerializer.Serialize(shoppingBasket));
        //    }
        //}
    }
}

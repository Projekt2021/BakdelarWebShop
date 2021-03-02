using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Bakdelar.Classes;

namespace Bakdelar.Pages
{
    public class ShoppingBasketModel : PageModel
    {
        public void OnGet()
        {
            var sessionBasket = HttpContext.Session.GetString("shopping_basket");
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };




            if (sessionBasket != null)
            {
                var shoppingBasket = JsonSerializer.Deserialize<ShoppingBasket>(sessionBasket, options);

                string stringu = JsonSerializer.Serialize(shoppingBasket, options);
            }
        }
    }
}

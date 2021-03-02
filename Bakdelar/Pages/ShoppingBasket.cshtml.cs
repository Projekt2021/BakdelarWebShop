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
        [BindProperty]
        public int NewCount { get; set; }
        public ShoppingBasket ShoppingBasket { get; set; }



        public void OnGet()
        {
            ShoppingBasket = GetBasket();
        }

        private ShoppingBasket GetBasket()
        {
            var sessionBasket = HttpContext.Session.GetString("shopping_basket");
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };




            if (sessionBasket == null)
            {
                return null;
            }

            var shoppingbasket = JsonSerializer.Deserialize<ShoppingBasket>(sessionBasket, options);
            return shoppingbasket;
           
        }


        public IActionResult OnPostChangeCount(int id)
        {
            ShoppingBasket = GetBasket();
            var product = ShoppingBasket.Items.Where(item => item.ID == id).FirstOrDefault();
            if(product == null)
            {
                return Page();
            }
            product.ItemCount = NewCount;
            HttpContext.Session.SetString("shopping_basket", JsonSerializer.Serialize(ShoppingBasket));
            return Page();
        }

        public IActionResult OnGetRemoveProduct(int id)
        {
            ShoppingBasket = GetBasket();

            var product = ShoppingBasket.Items.Where(item => item.ID == id).FirstOrDefault();
            ShoppingBasket.Items.Remove(product);
            HttpContext.Session.SetString("shopping_basket", JsonSerializer.Serialize(ShoppingBasket));
            return Page();

        }
    }
}

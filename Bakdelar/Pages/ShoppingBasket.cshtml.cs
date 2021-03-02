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
        public List<ShoppingBasketItem> ShoppingBasket { get; set; }



        public void OnGet()
        {
            ShoppingBasket = GetBasket();
        }

        private List<ShoppingBasketItem> GetBasket()
        {
            var sessionBasket = HttpContext.Session.GetString("shopping_basket");
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
            };




            if (sessionBasket == null)
            {
                return null;
            }

            var shoppingbasket = JsonSerializer.Deserialize<List<ShoppingBasketItem>>(sessionBasket, options);
            return shoppingbasket;
           
        }

        public void UpdateShoppingBasket(List<ShoppingBasketItem> shoppingBasket)
        {
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
            };
            HttpContext.Session.SetString("shopping_basket", JsonSerializer.Serialize(shoppingBasket, options));
        }






        public IActionResult OnPostChangeCount(int id)
        {
            ShoppingBasket = GetBasket();
            var product = ShoppingBasket.Where(item => item.ID == id).FirstOrDefault();
            if(product == null)
            {
                return Page();
            }
            product.ItemCount = NewCount;
            UpdateShoppingBasket(ShoppingBasket);
            return Page();
        }

        public IActionResult OnGetRemoveProduct(int id)
        {
            ShoppingBasket = GetBasket();

            var product = ShoppingBasket.Where(item => item.ID == id).FirstOrDefault();
            ShoppingBasket.Remove(product);
            UpdateShoppingBasket(ShoppingBasket);
            return Page();

        }
    }
}

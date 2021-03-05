using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Bakdelar.Classes;
using Bakdelar.MethodClasses;
using Microsoft.Extensions.Configuration;

namespace Bakdelar.Pages
{

    public class ShoppingBasketModel : PageModel
    {

        private IConfiguration _config { get; set; }




        [BindProperty]
        public int NewCount { get; set; }
        public List<ShoppingBasketItem> ShoppingBasket { get; set; }

        public ShoppingBasketModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {
            ShoppingBasket = HttpContext.Session.GetBasket();
        }

       

        public IActionResult OnPostChangeCount(int id)
        {
            
            ShoppingBasket = HttpContext.Session.GetBasket();
            var product = ShoppingBasket.Where(item => item.ID == id).FirstOrDefault();
            if(product == null)
            {
                return Page();
            }
            UpdateNumberSold(id, NewCount);
            
            product.ItemCount = NewCount;
            HttpContext.Session.UpdateShoppingBasket(ShoppingBasket);
            return Page();
        }

        public IActionResult OnGetRemoveProduct(int id)
        {
            ShoppingBasket = HttpContext.Session.GetBasket();
            var product = ShoppingBasket.Where(item => item.ID == id).FirstOrDefault();
            UpdateNumberSold(id, 0);
            ShoppingBasket.Remove(product);
            HttpContext.Session.UpdateShoppingBasket(ShoppingBasket);
            return Page();
        }


        private async void UpdateNumberSold(int id, int newNumberOfSold)
        {
            var product = await GetFromApi.GetProductAsync(id);
            product.NumberOfSold = newNumberOfSold;
            await GetFromApi.PutProductAsync(product);

            
        }
    }
}

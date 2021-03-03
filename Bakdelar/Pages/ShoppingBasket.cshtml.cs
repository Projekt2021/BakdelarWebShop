using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Bakdelar.Classes;
using Bakdelar.MethodClasses;

namespace Bakdelar.Pages
{

    public class ShoppingBasketModel : PageModel
    {
        [BindProperty]
        public int NewCount { get; set; }
        public List<ShoppingBasketItem> ShoppingBasket { get; set; }



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
            product.ItemCount = NewCount;
            HttpContext.Session.UpdateShoppingBasket(ShoppingBasket);
            return Page();
        }

        public IActionResult OnGetRemoveProduct(int id)
        {
            ShoppingBasket = HttpContext.Session.GetBasket();
            var product = ShoppingBasket.Where(item => item.ID == id).FirstOrDefault();
            ShoppingBasket.Remove(product);
            HttpContext.Session.UpdateShoppingBasket(ShoppingBasket);
            return Page();

        }
    }
}

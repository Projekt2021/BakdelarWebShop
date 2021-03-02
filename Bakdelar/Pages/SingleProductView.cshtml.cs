using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bakdelar.Classes;
using Bakdelar.MethodClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Bakdelar.Pages.Shared
{
    public class SingleProductViewModel : PageModel
    {






        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public SingleProductViewModel(
            IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            ILogger<IndexModel> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public ProductView Product { get; set; }

        public bool Error = false;


        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }


        [BindProperty]
        public ShoppingBasketItem ShoppingItem { get; set; }


        public async Task<IActionResult> OnGet()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    Product = await httpClient.GetFromJsonAsync<ProductView>($"{_configuration.GetValue<String>("APIEndpoint")}api/product/{ID}");
                }
                catch (Exception)
                {
                    if (Product.ProductName == null)
                    {
                        Error = true;
                    }
                }
            }

            return Page();

        }
        public IActionResult OnPost()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    Product = httpClient.GetFromJsonAsync<ProductView>($"{_configuration.GetValue<String>("APIEndpoint")}api/product/{ID}").Result;
                }
                catch (Exception)
                {
                    if (Product.ProductName == null)
                    {
                        Error = true;
                    }
                }
            }
            var shoppingBasket = HttpContext.Session.GetBasket();




            if (shoppingBasket != null)
            {

                var item = shoppingBasket.Where(item => item.ID == ShoppingItem.ID).FirstOrDefault();
                var totalItems = ShoppingItem.ItemCount;

                if (ShoppingItem == null)
                    return Redirect("/SingleProductView/" +ID);

                if (item != null)
                    item.ItemCount += ShoppingItem.ItemCount;
                else
                    shoppingBasket.Add(ShoppingItem);

                HttpContext.Session.UpdateShoppingBasket(shoppingBasket);
            }
            else
            {
                shoppingBasket = new List<ShoppingBasketItem>();
                shoppingBasket.Add(ShoppingItem);
                HttpContext.Session.UpdateShoppingBasket(shoppingBasket);
            }

            return Redirect("/SingleProductView/" + ID);
        }


        public async Task GetProduct()
        {
            using var httpClient = new HttpClient();

            Product = await httpClient.GetFromJsonAsync<ProductView>($"{ _configuration.GetValue<String>("APIEndpoint")}api/product/{ID}");
        }
    }
}

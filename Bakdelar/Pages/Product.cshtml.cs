using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bakdelar.Classes;

namespace Bakdelar.Pages
{
    public class ProductModel : PageModel
    {
        IConfiguration _configuration { get; set; }


        [BindProperty]
        public ShoppingBasketItem ShoppingItem { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }


        public ProductView Product { get; set; }

        public ProductModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task OnGetAsync()
        {
            await GetProduct();
        }
        public IActionResult OnPost()
        {
            var cookie = Request.Cookies["shopping_basket"];
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            if (cookie != null)
            {
                var shoppingBasket = JsonSerializer.Deserialize<ShoppingBasket>(cookie, options);

                var item = shoppingBasket.Items.Where(item => item.ID == ShoppingItem.ID).FirstOrDefault();
                var productExists = shoppingBasket.Items.Any(item => item.ID == ShoppingItem.ID);
                var totalItemCount = 0;

                if (ShoppingItem != null)
                    totalItemCount = ShoppingItem.ItemCount + item.ItemCount;

                Response.Cookies.Delete("shopping_basket");
                string stringu = JsonSerializer.Serialize(shoppingBasket, options);

                byte zeBytesOfZeStringu = Convert.ToByte(stringu);

                Response.Cookies.Append("shopping_basket", stringu);

            }
            else
            {
                var shoppingBasket = new ShoppingBasket();
                shoppingBasket.Items = new List<ShoppingBasketItem>();
                shoppingBasket.Items.Add(ShoppingItem);
                Response.Cookies.Append("shopping_basket", JsonSerializer.Serialize(shoppingBasket, options));
            }

            return Redirect("/Product?id=" + ID);
        }


        public async Task GetProduct()
        {
            using var httpClient = new HttpClient();

            Product = await httpClient.GetFromJsonAsync<ProductView>($"{ _configuration.GetValue<String>("APIEndpoint")}api/product/{ID}");
        }
    }
}

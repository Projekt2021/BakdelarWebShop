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

namespace Bakdelar.Pages
{
    public class ProductModel : PageModel
    {
        IConfiguration _configuration { get; set; }


        [BindProperty]
        public Classes.ShoppingBasketItem ShoppingItem { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }


        public Classes.ProductView Product { get; set; }

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
                
                var shoppingBasket = JsonSerializer.Deserialize<Classes.ShoppingBasket>(cookie, options);
                if(shoppingBasket.Items.Any(item => item.ID == ShoppingItem.ID))
                {
                    var item = shoppingBasket.Items.Where(i => i.ID == ShoppingItem.ID).FirstOrDefault();
                    item.ItemCount += ShoppingItem.ItemCount;
                }
                else
                {
                    shoppingBasket.Items.Add(ShoppingItem);
                }

                Response.Cookies.Delete("shopping_basket");
                string c = JsonSerializer.Serialize(shoppingBasket, options);
                Response.Cookies.Append("shopping_basket", JsonSerializer.Serialize(shoppingBasket, options)); 
                var bytes = System.Text.ASCIIEncoding.ASCII.GetByteCount(c);
            }
            else
            {
                var shoppingBasket = new Classes.ShoppingBasket();
                shoppingBasket.Items = new List<Classes.ShoppingBasketItem>();
                shoppingBasket.Items.Add(ShoppingItem);
                Response.Cookies.Append("shopping_basket", JsonSerializer.Serialize(shoppingBasket, options));
            }

           return Redirect("/Product?id=" + ID);
        }


        public async Task GetProduct()
        {
            using var httpClient = new HttpClient();
            Product = await httpClient.GetFromJsonAsync<Classes.ProductView>($"{ _configuration.GetValue<String>("APIEndpoint")}api/product/{ID}");
        }
    }
}

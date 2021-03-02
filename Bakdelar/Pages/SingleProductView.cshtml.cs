﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bakdelar.Classes;
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
            var sessionBasket = HttpContext.Session.GetString("shopping_basket");
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };




            if (sessionBasket != null)
            {
                var shoppingBasket = JsonSerializer.Deserialize<ShoppingBasket>(sessionBasket, options);

                var item = shoppingBasket.Items.Where(item => item.ID == ShoppingItem.ID).FirstOrDefault();
                var totalItems = ShoppingItem.ItemCount;

                if (ShoppingItem == null)
                    return Redirect("/SingleProductView/" +ID);

                //if (item != null)
                //    totalItems = item.ItemCount += ShoppingItem.ItemCount;

                    //if (totalItems < 1)
                    //    shoppingBasket.Items.Remove(item);
                if (item != null)
                    item.ItemCount += ShoppingItem.ItemCount;
                else
                    shoppingBasket.Items.Add(ShoppingItem);

                string stringu = JsonSerializer.Serialize(shoppingBasket, options);


                HttpContext.Session.SetString("shopping_basket", stringu);
            }
            else
            {
                var shoppingBasket = new ShoppingBasket();
                shoppingBasket.Items = new List<ShoppingBasketItem>();
                shoppingBasket.Items.Add(ShoppingItem);

                HttpContext.Session.SetString("shopping_basket", JsonSerializer.Serialize(shoppingBasket, options));
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

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
            Product = GetFromApi.GetProductAsync(ID).Result;
            await SetBreadcrumb();
            return Page();

        }
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        try
        //        {
        //            Product = await GetFromApi.GetProductAsync(ID);
        //        }
        //        catch (Exception)
        //        {
        //            if (Product.ProductName == null)
        //            {
        //                Error = true;
        //            }
        //        }
        //    }
        //    var shoppingBasket = HttpContext.Session.GetBasket();

        //    bool itemAlreadyInBasket = ItemAlreadyInBasket(shoppingBasket);
        //    bool tooManyItemsAdded = false;
        //    ShoppingBasketItem item;
        //    if(itemAlreadyInBasket)
        //    {
        //        item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();
        //        tooManyItemsAdded = item.ItemCount + ShoppingItem.ItemCount > Product.AvailableQuantity.Value;
        //    }


        //    if (ShoppingItem.ItemCount > Product.AvailableQuantity.Value - Product.NumberOfSold)
        //    {
        //        return null;
        //    }


        //    if(shoppingBasket == null)
        //    {
        //        shoppingBasket = new();
        //        shoppingBasket.Add(ShoppingItem);
        //        Product.NumberOfSold += ShoppingItem.ItemCount;
        //    }
        //    else if (!itemAlreadyInBasket)
        //    {
        //        shoppingBasket.Add(ShoppingItem);
        //        Product.NumberOfSold += ShoppingItem.ItemCount;
        //    }
        //    else if(tooManyItemsAdded)
        //    {
        //        item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();
        //        item.ItemCount = Product.AvailableQuantity.Value;
        //        Product.NumberOfSold = Product.AvailableQuantity.Value;
        //    }
        //    else
        //    {
        //        item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();

        //        Product.NumberOfSold += ShoppingItem.ItemCount;
        //        item.ItemCount += ShoppingItem.ItemCount;
        //    }
        //    HttpContext.Session.UpdateShoppingBasket(shoppingBasket);
        //    await GetFromApi.PutProductAsync(Product);
        //    ModelState.Clear();

        //    //if (shoppingBasket != null)
        //    //{

        //    //    var item = shoppingBasket.Where(item => item.ID == ShoppingItem.ID).FirstOrDefault();
        //    //    var totalItems = ShoppingItem.ItemCount;

        //    //    if (ShoppingItem == null)
        //    //        return Redirect("/SingleProductView/" +ID);







        //    //    if (item != null)
        //    //    {
        //    //        if (item.ItemCount + ShoppingItem.ItemCount > Product.AvailableQuantity.Value - Product.NumberOfSold)
        //    //        {
        //    //            item.ItemCount = Product.AvailableQuantity.Value;
        //    //            Product.NumberOfSold = item.ItemCount;
        //    //        }
        //    //        else
        //    //        {
        //    //            item.ItemCount += ShoppingItem.ItemCount;
        //    //            Product.NumberOfSold = item.ItemCount;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        shoppingBasket.Add(ShoppingItem);
        //    //        Product.NumberOfSold += ShoppingItem.ItemCount;
        //    //    }

        //    //    HttpContext.Session.UpdateShoppingBasket(shoppingBasket);
        //    //}
        //    //else
        //    //{
        //    //    shoppingBasket = new List<ShoppingBasketItem>();
        //    //    shoppingBasket.Add(ShoppingItem);
        //    //    Product.NumberOfSold = ShoppingItem.ItemCount;
        //    //    HttpContext.Session.UpdateShoppingBasket(shoppingBasket);
        //    //}
        //    //using (var httpClient = new HttpClient())
        //    //{
        //    //    await httpClient.PutAsJsonAsync($"{_configuration.GetValue<String>("APIEndpoint")}api/product/{ID}", Product);
        //    //}
        //    //ModelState.Clear();
        //    return Redirect("/SingleProductView/" + ID);
        //}


        public async Task GetProduct()
        {
            using var httpClient = new HttpClient();

            Product = await GetFromApi.GetProductAsync(ID);
        }



        public bool ItemAlreadyInBasket(List<ShoppingBasketItem> shoppingBasket)
        {
            if (shoppingBasket == null || !shoppingBasket.Any())
            {
                return false;
            }
            else
            {
                return shoppingBasket.Any(item => item.ID == ShoppingItem.ID);
            }
        }



        private async Task SetBreadcrumb()
        {
            if (Product != null)
            {
                ViewData["breadcrumb"] = new BreadcrumbsView() { Category = Product.Category, ProductName= Product.ProductName };
            }
        }
    }
}

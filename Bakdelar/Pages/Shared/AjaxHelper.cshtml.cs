using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bakdelar.Classes;
using Bakdelar.MethodClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Bakdelar.Pages.Shared
{
    public class AjaxHelperModel : PageModel
    {
        ISession _session { get; set; }
        IConfiguration _configuration { get; set; }


        [BindProperty]
        public Classes.ShoppingBasketItem ShoppingItem { get; set; }
        public Classes.ProductView Product { get; set; }
        public AjaxHelperModel(IHttpContextAccessor context, IConfiguration config)
        {
            _session = context.HttpContext.Session;
            _configuration = config;
        }
        public List<Classes.ShoppingBasketItem> ShoppingBasket { get; set; }
        public void OnGet()
        {
        }

        public PartialViewResult OnPostUpdateBasket()
        {
            
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    Product = httpClient.GetFromJsonAsync<ProductView>($"{_configuration.GetValue<String>("APIEndpoint")}api/product/{ShoppingItem.ID}").Result;
                }
                catch (Exception)
                {
                    if (Product.ProductName == null)
                    {
                    }
                }
            }
            ShoppingBasket = MethodClasses.SessionMethods.GetBasket(_session);
            if (ShoppingBasket != null)
            {

                var item = ShoppingBasket.Where(item => item.ID == ShoppingItem.ID).FirstOrDefault();
                var totalItems = ShoppingItem.ItemCount;

                if (ShoppingItem == null)
                    return null;

                if (item != null)
                {
                    if (item.ItemCount + ShoppingItem.ItemCount > Product.AvailableQuantity.Value)
                    {
                        item.ItemCount = Product.AvailableQuantity.Value;
                    }
                    else
                    {
                        item.ItemCount += ShoppingItem.ItemCount;
                    }
                }
                else
                {
                    ShoppingBasket.Add(ShoppingItem);
                }

                HttpContext.Session.UpdateShoppingBasket(ShoppingBasket);
            }
            else
            {
                ShoppingBasket = new List<ShoppingBasketItem>();
                ShoppingBasket.Add(ShoppingItem);
                HttpContext.Session.UpdateShoppingBasket(ShoppingBasket);
            }
            return Partial("_ShoppingBasketAjax", ShoppingBasket);
        }
    }
}

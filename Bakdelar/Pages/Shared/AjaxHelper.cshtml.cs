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





        [BindProperty]
        public int RemoveItemID { get; set; }



        [BindProperty]
        public int UpdateStockProductID { get; set; }



        [BindProperty]
        public int UpdateCountID { get; set; }





        public AjaxHelperModel(IHttpContextAccessor context, IConfiguration config)
        {
            _session = context.HttpContext.Session;
            _configuration = config;
        }











        public void OnGet()
        {
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






        public async Task<PartialViewResult> OnPostRemoveItemAsync()
        {

            var shoppingBasket = HttpContext.Session.GetBasket();

            Product = await GetFromApi.GetProductAsync(RemoveItemID);
            Product.NumberOfSold = 0;
            await GetFromApi.PutProductAsync(Product);
            var itemToRemove = shoppingBasket.FirstOrDefault(p => p.ID == RemoveItemID);
            shoppingBasket.Remove(itemToRemove);
            HttpContext.Session.UpdateShoppingBasket(shoppingBasket);
            return Partial("_InnerShoppingBasket", shoppingBasket);

        }

        public PartialViewResult OnPostItemCount()
        {

            var shoppingBasket = HttpContext.Session.GetBasket();
            return Partial("_ShoppingBasketItemCount", shoppingBasket);

        }





        public async Task<PartialViewResult> OnPostGetNumberInStockAsync()
        {
            var product = await GetFromApi.GetProductAsync(UpdateStockProductID);
            return Partial("_GetNumberInStock", product);

        }


        public PartialViewResult OnPostGetTotalCost()
        {
            var shoppingBasket = SessionMethods.GetBasket(_session);
            return Partial("_ShoppingBasketTotalPrice", shoppingBasket);

        }



        public async Task<PartialViewResult> OnPostDecreaseByOneAsync()
        {

            return await UpdateSingleShoppingItem(UpdateCountID, -1);
            

        }





        public async Task<PartialViewResult> OnPostIncreaseByOneAsync()
        {

            return await UpdateSingleShoppingItem(UpdateCountID, 1);

        }







        private async Task<PartialViewResult> UpdateSingleShoppingItem(int id, int newAmount)
        {
            var shoppingBasket = HttpContext.Session.GetBasket();
            var shoppingItem = shoppingBasket.FirstOrDefault(p => p.ID == UpdateCountID);
            shoppingItem.ItemCount += newAmount;

            Product = await GetFromApi.GetProductAsync(id);
            if (shoppingItem.ItemCount == 0)
            {
                shoppingBasket.Remove(shoppingItem);
            }
            else if (shoppingItem.ItemCount > Product.AvailableQuantity)
            {
                shoppingItem.ItemCount = Product.AvailableQuantity.Value;
            }
            Product.NumberOfSold = shoppingItem.ItemCount;
            await GetFromApi.PutProductAsync(Product);
            SessionMethods.UpdateShoppingBasket(_session, shoppingBasket);
            return Partial("_UpdateItemCount", shoppingItem);
        }















        public async Task<PartialViewResult> OnPostUpdateBasketAsync()
        {

            //skapar ett nytt objekt med default v�rden, f�r att kolla om ShoppingItem �r "null"
            var defaultShoppingItemValues = new ShoppingBasketItem();
            var def = defaultShoppingItemValues.Equals(ShoppingItem);
            var shoppingBasket = HttpContext.Session.GetBasket();
            if (!def)
            { 
            int ID = ShoppingItem.ID;
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    Product = await GetFromApi.GetProductAsync(ID);
                }
                catch (Exception)
                {
                    if (Product.ProductName == null)
                    {
                    }
                }
            }

            bool itemAlreadyInBasket = ItemAlreadyInBasket(shoppingBasket);
            bool tooManyItemsAdded = false;
            ShoppingBasketItem item;
            if (itemAlreadyInBasket)
            {
                item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();
                tooManyItemsAdded = item.ItemCount + ShoppingItem.ItemCount > Product.AvailableQuantity.Value;
            }


            if (ShoppingItem.ItemCount > Product.AvailableQuantity.Value - Product.NumberOfSold)
            {
                return Partial("_InnerShoppingBasket", shoppingBasket);
            }


            if (shoppingBasket == null)
            {
                shoppingBasket = new();
                shoppingBasket.Add(ShoppingItem);
                Product.NumberOfSold += ShoppingItem.ItemCount;
            }
            else if (!itemAlreadyInBasket)
            {
                shoppingBasket.Add(ShoppingItem);
                Product.NumberOfSold += ShoppingItem.ItemCount;
            }
            else if (tooManyItemsAdded)
            {
                item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();
                item.ItemCount = Product.AvailableQuantity.Value;
                Product.NumberOfSold = Product.AvailableQuantity.Value;
            }
            else
            {
                item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();

                Product.NumberOfSold += ShoppingItem.ItemCount;
                item.ItemCount += ShoppingItem.ItemCount;
            }

            HttpContext.Session.UpdateShoppingBasket(shoppingBasket);

            await GetFromApi.PutProductAsync(Product);
            }
            return Partial("_InnerShoppingBasket", shoppingBasket);
        }
    }
}

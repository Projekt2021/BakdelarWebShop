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
        public ShoppingBasketItem ShoppingItem { get; set; }
        public ProductView Product { get; set; }





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

            var itemToRemove = shoppingBasket.FirstOrDefault(p => p.ID == RemoveItemID);
            //Product = await GetFromApi.GetProductAsync(RemoveItemID);
            //Product.NumberOfSold -= itemToRemove.ItemCount;
            //await GetFromApi.PutProductAsync(Product);
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

        public PartialViewResult OnPostUpdateShoppingBasketPage()
        {
            var shoppingBasket = HttpContext.Session.GetBasket();
            return Partial("_ShoppingBasketEditPage", shoppingBasket);


        }



        public async Task<PartialViewResult> OnPostIncreaseByOneAsync()
        {

            return await UpdateSingleShoppingItem(UpdateCountID, 1);

        }


        public async Task<PartialViewResult> OnPostUpdateItemCountShoppingBasket(int itemID, int newAmount)
        {
            var shoppingBasket = HttpContext.Session.GetBasket();
            if (itemID == 0)
            {

                return Partial("_ShoppingBasketEditPage", shoppingBasket);
            }
            var shoppingItem = shoppingBasket.FirstOrDefault(p => p.ID == itemID);
            if (newAmount == 0)
            {
                shoppingBasket.Remove(shoppingItem);
            }
            else
            {
                shoppingItem.ItemCount = newAmount;
            }

            Product = await GetFromApi.GetProductAsync(itemID);

            Product.NumberOfSold = newAmount;
            if (shoppingItem.ItemCount > Product.AvailableQuantity)
            {
                shoppingItem.ItemCount = Product.AvailableQuantity.Value;
                Product.NumberOfSold = Product.AvailableQuantity.Value;
            }

            await GetFromApi.PutProductAsync(Product);
            SessionMethods.UpdateShoppingBasket(_session, shoppingBasket);
            return Partial("_ShoppingBasketEditPage", shoppingBasket);
        }




        private async Task<PartialViewResult> UpdateSingleShoppingItem(int id, int newAmount)
        {
            var shoppingBasket = HttpContext.Session.GetBasket();
            var shoppingItem = shoppingBasket.FirstOrDefault(p => p.ID == UpdateCountID);

            Product = await GetFromApi.GetProductAsync(id);





            int maxAllowed = (Product.AvailableQuantity.Value - Product.NumberOfSold);


            //om man försöker lägga till fler än vad som finns på lager
            if (newAmount <= maxAllowed && newAmount > 0)
            {
                shoppingItem.ItemCount += newAmount;


                //switch(newAmount)
                //{
                //    case -1:
                //    case 1:
                //        Product.NumberOfSold+=newAmount;
                //        break;


                //    default:
                //        Product.NumberOfSold = newAmount;
                //        break;

                //}
                if (newAmount == 1)
                {
                    Product.NumberOfSold++;

                }
                else
                {
                    Product.NumberOfSold = newAmount;
                }
                await GetFromApi.PutProductAsync(Product);


                //if (shoppingItem.ItemCount > Product.AvailableQuantity)
                //{
                //    shoppingItem.ItemCount = Product.AvailableQuantity.Value;
                //}
                SessionMethods.UpdateShoppingBasket(_session, shoppingBasket);

            }
            else if (newAmount == -1)
            {
                shoppingItem.ItemCount--;
                Product.NumberOfSold--;

                await GetFromApi.PutProductAsync(Product);
                if (shoppingItem.ItemCount == 0)
                {
                    shoppingBasket.Remove(shoppingItem);
                }

                SessionMethods.UpdateShoppingBasket(_session, shoppingBasket);
            }
            return Partial("_UpdateItemCount", shoppingItem);
        }









        public async Task<IActionResult> OnPostChangeCountItemAsync(int id, int newAmount)
        {

            var shoppingBasket = HttpContext.Session.GetBasket();
            var shoppingItem = shoppingBasket.FirstOrDefault(p => p.ID == id);

            Product = await GetFromApi.GetProductAsync(id);

            //int maxAllowed = (Product.AvailableQuantity.Value - Product.NumberOfSold) + shoppingItem.ItemCount;
            int maxAllowed = Product.AvailableQuantity.Value;
            int newAdded = newAmount - shoppingItem.ItemCount;


            if (newAmount > maxAllowed)
            {
                newAmount = maxAllowed;
            }


            shoppingItem.ItemCount = newAmount;
            Product.NumberOfSold += newAdded;


            await GetFromApi.PutProductAsync(Product);

            decimal costForItems = shoppingItem.Price * shoppingItem.ItemCount;
            decimal totalCostForAllItems = shoppingBasket.Sum(item => item.ItemCount * item.Price);
            if (totalCostForAllItems < 300)
            {
                totalCostForAllItems += StaticValues.ShippingFee;
            }

            await GetFromApi.PutProductAsync(Product);
            _session.UpdateShoppingBasket(shoppingBasket);
            return new JsonResult(new { costItems = $"{costForItems} kr", totalCost = $"{totalCostForAllItems} kr" });
        }







        public PartialViewResult OnPostGetShippingCostBanner()
        {

            var shoppingBasket = HttpContext.Session.GetBasket();
            return Partial("_ShoppingBasketShippingBanner", shoppingBasket);
        }

        public PartialViewResult OnPostGetShippingCostRow()
        {

            var shoppingBasket = HttpContext.Session.GetBasket();

            decimal totalCostForAllItems = shoppingBasket.Sum(item => item.ItemCount * item.Price);
            return Partial("_ShippingBasketShippingCostRow", totalCostForAllItems);
        }



        public async Task<PartialViewResult> OnPostUpdateBasketAsync()
        {

            //skapar ett nytt objekt med default värden, för att kolla om ShoppingItem är "null"

            List<ShoppingBasketItem> shoppingBasket;
            var defaultShoppingItemValues = new ShoppingBasketItem();
            var def = defaultShoppingItemValues.Equals(ShoppingItem);

            if (!def)
            {
                int ID = ShoppingItem.ID;
                Product = await GetFromApi.GetProductAsync(ID);

                shoppingBasket = HttpContext.Session.GetBasket();
                bool itemAlreadyInBasket = ItemAlreadyInBasket(shoppingBasket);
                bool tooManyItemsAdded;

                //if (shoppingBasket != null)
                //{
                //    ShoppingBasketItem item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();
                //    tooManyItemsAdded = item.ItemCount + ShoppingItem.ItemCount > Product.AvailableQuantity.Value ? tooManyItemsAdded = true : tooManyItemsAdded;
                //}

                if (shoppingBasket != null)
                {
                    ShoppingBasketItem item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();
                    tooManyItemsAdded = item.ItemCount + ShoppingItem.ItemCount > Product.AvailableQuantity.Value ? tooManyItemsAdded = true : tooManyItemsAdded = false;

                    if (tooManyItemsAdded)
                        return Partial("_InnerShoppingBasket", shoppingBasket);

                    if (itemAlreadyInBasket)
                    {
                        item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();

                        Product.NumberOfSold += ShoppingItem.ItemCount;
                        item.ItemCount += ShoppingItem.ItemCount;
                    }
                    else
                        shoppingBasket.Add(ShoppingItem);
                }
                else
                {
                    shoppingBasket = new();
                    tooManyItemsAdded = ShoppingItem.ItemCount > Product.AvailableQuantity.Value ? tooManyItemsAdded = true : tooManyItemsAdded = false;
                    
                    if (!tooManyItemsAdded)
                        shoppingBasket.Add(ShoppingItem);

                    return Partial("_InnerShoppingBasket", shoppingBasket);
                    
                    //else
                    //{
                    //    shoppingBasket = new();
                    //    shoppingBasket.Add(ShoppingItem);
                    //    ShoppingBasketItem item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();
                    //    tooManyItemsAdded = item.ItemCount + ShoppingItem.ItemCount > Product.AvailableQuantity.Value ? tooManyItemsAdded = true : tooManyItemsAdded;
                    //}
                }
                
            //    else if (!itemAlreadyInBasket)
            //    {
            //        if (shoppingBasket == null)
            //        {
            //            shoppingBasket = new();
            //            shoppingBasket.Add(ShoppingItem);
            //            //Product.NumberOfSold += ShoppingItem.ItemCount;
            //        }
            //        else
            //            shoppingBasket.Add(ShoppingItem);
            //            //Product.NumberOfSold += ShoppingItem.ItemCount;
            //    }
                
            //    if (tooManyItemsAdded)
            //    {
            //        item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();
            //        item.ItemCount = Product.AvailableQuantity.Value;
            //        //Product.NumberOfSold = Product.AvailableQuantity.Value;
            //    }
            //    else
            //    {
            //        item = shoppingBasket.Where(shoppingItem => shoppingItem.ID == ShoppingItem.ID).FirstOrDefault();

            //        Product.NumberOfSold += ShoppingItem.ItemCount;
            //        item.ItemCount += ShoppingItem.ItemCount;
            //    }

            //    HttpContext.Session.UpdateShoppingBasket(shoppingBasket);

            //    await GetFromApi.PutProductAsync(Product);
            //
            }

            return Partial("_InnerShoppingBasket", HttpContext.Session.GetBasket());
        }
    }
}

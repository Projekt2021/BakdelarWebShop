using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakdelar.Areas.Identity.Data;
using Bakdelar.Classes;
using Bakdelar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bakdelar.Pages
{
    public class OrderConfirmationModel : PageModel
    {


        public OrderDbContext _context { get; set; }
        public UserManager<MyUser> _userManager { get; set; }

        //public SignInManager<MyUser> _signInManager { get; set; }

        public OrderConfirmationModel(OrderDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }
        public bool AlreadyViewed { get; set; }

        public Dictionary<int, string> ProductImages { get; set; }

        public Order Order { get; set; }






        public void OnGet()
        {
            Order = _context.Orders.Where(order => order.OrderID == ID).Include(order => order.OrderItems).FirstOrDefault();
            if (Order != null)
            {
                //"9aca5242-25b3-43a3-aa4c-b2f226c3a970"
                string userID = _userManager.GetUserId(User);
                bool sameUser = (!string.IsNullOrWhiteSpace(Order.UserID) &&
                                 !string.IsNullOrWhiteSpace(userID)) &&
                                 (Order.UserID == userID);





                //if ((Order.HasBeenViewed && !sameUser) || !sameUser)
                //{
                //    Order = null;
                //}
                //else 

                var isAdmin = _userManager.IsInRoleAsync(_userManager.GetUserAsync(User).Result, "Admin").Result;

                //bool isAdmin = isAdminASync.Result;


                if (!Order.HasBeenViewed || sameUser|| isAdmin)
                {
                    AlreadyViewed = Order.HasBeenViewed;
                    Order.HasBeenViewed = true;
                    _context.SaveChanges();
                    ProductImages = new Dictionary<int, string>();
                    foreach (var item in Order.OrderItems)
                    {
                        ProductView product = MethodClasses.GetFromApi.GetProductAsync(item.ProductID).Result;
                        ProductImages.Add(item.ProductID, product.ProductImageView.FirstOrDefault().ImageURL);
                    }
                }
               
                else
                {
                    Order = null;
                }
            }
        }
    }
}

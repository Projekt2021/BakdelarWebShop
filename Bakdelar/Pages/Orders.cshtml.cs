using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakdelar.Areas.Identity.Data;
using Bakdelar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakdelar.Areas.Identity.Pages.Account
{
    [Authorize]
    public class OrdersModel : PageModel
    {

        public List<Order> UserOrders { get; set; }

        public bool IsAdmin { get; set; }

        public decimal SumOfSales { get; set; }
        public decimal TotalDiscounted { get; set; }
        public decimal TotalDiscountedOrders { get; set; }
        public int NumberOfOrders{ get; set; }
        public int NumberOfShippingFees { get; set; }
        public decimal TotalShippingFee { get; set; }
        public int NumberOfCustomers { get; set; }
        public int ReturningCustomers { get; set; }
        public int NumberOfSignedInOrders { get; set; }



        public UserManager<MyUser> _userManager { get; set; }
        public AuthenticationDbContext _authDbContext { get; set; }
        public SignInManager<MyUser> _signInManager { get; set; }
        public OrderDbContext _orderDbContext { get; set; }
        public OrdersModel(UserManager<MyUser> userManager, AuthenticationDbContext authenticationDbContext, 
                           OrderDbContext orderDbContext, SignInManager<MyUser> signInManager)
        {
            _userManager = userManager;
            _authDbContext = authenticationDbContext;
            _orderDbContext = orderDbContext;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
            string userID = _userManager.GetUserId(User); 
            var user = _userManager.GetUserAsync(User).Result;

            IsAdmin = _userManager.IsInRoleAsync(user, "Admin").Result;

            if (IsAdmin)
            {
                UserOrders = _orderDbContext.Orders.OrderByDescending(o => o.OrderDate).ToList();

                TotalDiscounted = UserOrders.Sum(o => o.CouponValue);

                TotalDiscountedOrders = UserOrders.Count(o => o.CouponUsed);

                SumOfSales = UserOrders.Sum(o => o.OrderCost - o.ShippingFee);

                NumberOfOrders = UserOrders.Count;

                NumberOfShippingFees = UserOrders.Where(o => o.ShippingPaid == true)
                                        .Count();

                TotalShippingFee = UserOrders.Sum(o => o.ShippingFee);

                NumberOfCustomers = UserOrders.GroupBy(g => g.CustomerEmail)
                                        .Count();

                ReturningCustomers = UserOrders.GroupBy(o => o.CustomerEmail)
                                        .Where(g => g.Count() > 1)
                                        .Count();

                NumberOfSignedInOrders = UserOrders.Where(o => o.UserID != null).Count();

                //ReturningCustomers = UserOrders.GroupBy(o => o.UserID)
                //                        .Where(g => g.Count() > 1 && g.Key != null)
                //                        .Count();

            }
            else
            {
                UserOrders = _orderDbContext.Orders.Where(order => order.UserID == userID)
                                .OrderByDescending(o => o.OrderDate).ToList();
            }
        }
    }
}

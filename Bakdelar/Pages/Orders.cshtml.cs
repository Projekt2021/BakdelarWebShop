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
            if (_userManager.IsInRoleAsync(user, "Admin").Result)
            {
                UserOrders = _orderDbContext.Orders.ToList();
            }
            else
            {
                UserOrders = _orderDbContext.Orders.Where(order => order.UserID == userID).ToList();
            }
        }
    }
}

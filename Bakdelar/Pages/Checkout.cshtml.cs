using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakdelar.MethodClasses;
using Bakdelar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Bakdelar.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Bakdelar.Classes;

namespace Bakdelar.Pages
{
    public class CheckoutModel : PageModel
    {
        public UserManager<MyUser> _userManager { get; set; }
        public SignInManager<MyUser> _signInManager { get; set; }
        public OrderDbContext _context { get; set; }

        public List<ShoppingBasketItem> ShoppingBasket { get; set; }

        public CheckoutModel(OrderDbContext context, UserManager<MyUser> userManager, SignInManager<MyUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public void OnGet()
        {
            ShoppingBasket = HttpContext.Session.GetBasket();
            if (_signInManager.IsSignedIn(User))
            {
                var user = _userManager.Users
                                      .Include(a => a.Address)
                                      .Single(x => x.NormalizedEmail == _userManager.GetUserAsync(User).Result.NormalizedEmail);
                ViewData["email"] = user.Email;
                var address = user.Address;
                ViewData["street"] = address?.Street;
                ViewData["zip"] = address?.ZipCode;
                ViewData["city"] = address?.City;
                ViewData["firstname"] = user.FirstName;
                ViewData["lastName"] = user.LastName;
                ViewData["phonenumber"] = user.PhoneNumber;
            }

        }

        //för att spara data som användaren skriver in i fältet
        [BindProperty]
        public Customer Customer { get; set; }


        public async Task<IActionResult> OnPost()
        {
            var session = HttpContext.Session;
            string paymentMethod = "";
            DateTime orderDate = DateTime.UtcNow;
            List<OrderItem> orderItems = session.GetBasket()
                                                 .Select(item => new OrderItem(item))
                                                 .ToList();
            decimal orderCost = orderItems.Sum(item => item.ProductPricePaidTotal);
            bool shippingPaid = orderCost < 300;


            string userID = null;


            if (_signInManager.IsSignedIn(User))
            {
                userID = _userManager.GetUserId(User);
            }

            decimal shippingFee = StaticValues.ShippingFee;
            if(shippingPaid)
            {
                orderCost += shippingFee;
            }
            else
            {
                shippingFee = 0;
            }

            var order = new Order()
            {
                CustomerFirstName = Customer.FirstName,
                CustomerLastName = Customer.LastName,
                CustomerEmail = Customer.Email,
                CustomerAddress = Customer.Address,
                CustomerCOAddress = Customer.AddressCO,
                CustomerZipCode = Customer.ZipCode,
                CustomerCity = Customer.City,
                PaymentMethod = paymentMethod,
                OrderDate = orderDate,
                ShippingPaid = shippingPaid,
                ShippingFee = shippingFee,
                OrderCost = orderCost,
                OrderItems = orderItems,
                UserID = userID
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            session.Clear();
            return Redirect($"~/OrderConfirmation/{order.OrderID}");
        }
    }
}

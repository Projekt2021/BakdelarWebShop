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

namespace Bakdelar.Pages
{
    public class CheckoutModel : PageModel
    {
        public IServiceProvider _service { get; set; }
        public OrderDbContext _context { get; set; }

        public void OnGet([FromServices] OrderDbContext context, [FromServices] UserManager<MyUser> userManager, [FromServices] SignInManager<MyUser> signInManager)
        {
            _context = context;
            if (signInManager.IsSignedIn(User))
            {
                ViewData["email"] = userManager.GetUserAsync(User).Result.Email;
            }

        }


        [BindProperty]
        public Customer Customer { get; set; }


        public async Task<IActionResult> OnPost([FromServices] OrderDbContext context)
        {
            var session = HttpContext.Session;
            _context = context;
            string paymentMethod = "";
            DateTime orderDate = DateTime.UtcNow;
            List<OrderItem> orderItems = session.GetBasket()
                                                 .Select(item => new OrderItem(item))
                                                 .ToList();
            decimal orderCost = orderItems.Sum(item => item.ProductPricePaidTotal);
            bool shippingPaid = orderCost < 300;

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
                CustomerZipCode = Customer.ZipCode,
                CustomerCity = Customer.City,
                PaymentMethod = paymentMethod,
                OrderDate = orderDate,
                ShippingPaid = shippingPaid,
                ShippingFee = shippingFee,
                OrderCost = orderCost,
                OrderItems = orderItems
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            session.Clear();
            return Redirect($"~/OrderConfirmation/{order.OrderID}");
        }
    }
}

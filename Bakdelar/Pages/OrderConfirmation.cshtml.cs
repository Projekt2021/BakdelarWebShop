using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakdelar.Classes;
using Bakdelar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bakdelar.Pages
{
    public class OrderConfirmationModel : PageModel
    {


        public OrderDbContext _context { get; set; }



        public OrderConfirmationModel(OrderDbContext context)
        {
            _context = context;
        }



        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }

        public Dictionary<int, string> ProductImages { get; set; }

        public Order Order { get; set; }






        public void OnGet()
        {
            Order = _context.Orders.Where(order => order.OrderID == ID).Include(order => order.OrderItems).FirstOrDefault();
            if (Order != null)
            {
                ProductImages = new Dictionary<int, string>();
                foreach (var item in Order.OrderItems)
                {
                    ProductView product = MethodClasses.GetFromApi.GetProductAsync(item.ProductID).Result;
                    ProductImages.Add(item.ProductID, product.ProductImageView.FirstOrDefault().ImageURL);
                }
            }
        }
    }
}

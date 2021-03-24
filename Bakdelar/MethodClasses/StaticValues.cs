using Bakdelar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.MethodClasses
{
    public class StaticValues
    {

        const decimal shippingFee = 49.00M;
        private static OrderDbContext _context;


        public static decimal ShippingFee { get { return shippingFee; } }



        public static void AddDbContext(OrderDbContext context)
        {
            _context = context;
        }


        public static OrderDbContext GetContext()
        {
            return _context;
        }
    }
}

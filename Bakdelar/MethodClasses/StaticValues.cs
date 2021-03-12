using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.MethodClasses
{
    public class StaticValues
    {

        const decimal shippingFee = 49.00M;

        public static decimal ShippingFee { get { return shippingFee; } }
    }
}

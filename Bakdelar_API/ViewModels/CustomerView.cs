using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar_API.ViewModels
{
    public class CustomerView
    {
        public int CustomerId { get; set; }

        public string UserAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public Guid UserId { get; set; }


        //public List Carts { get; set; }

    }
}

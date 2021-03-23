using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Classes
{
    public class CustomerView
    {
        public int CustomerId { get; set; }
       
        public Guid UserId { get; set; }

        [Phone]
        [Display(Name = "Phone number"), Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "First Name"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name"), Required]
        public string LastName { get; set; }

        [Display(Name = "Address"), Required]
        public string Address { get; set; }
    }
}

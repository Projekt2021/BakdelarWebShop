using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Models
{
    public class Customer
    {
        [Required]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Förnamn är obligatoriskt") ]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Gatuadress")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Postnummer")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Postort")]
        public string City { get; set; }

        [Display(Name = "C/O")]
        public string AddressCO { get; set; }

        //[Required]
        //[Display(Name = "Postnummer C/O")]
        //public string ZipCodeCO { get; set; }

        //[Required]
        //[Display(Name = "Postort C/O")]
        //public string CityCO { get; set; }




        [Required]
        [Display(Name = "E-post")]
        [EmailAddress(ErrorMessage = "Ange en giltig E-post")]
        public string Email { get; set; }
    }
}

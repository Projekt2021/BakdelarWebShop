using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Areas.Identity.Data
{
    public class MyUser : IdentityUser
    {

        //derivative of IdentityUser with more user data
        [Required]
        [Display(Name = "Förnamn *")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Efternamn *")]
        public string LastName { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("MyUser")]
        public string MyUserID { get; set; }

        [Display(Name = "Gatuadress")]
        public string Street { get; set; }
        [Display(Name = "Postnummer")]
        public string ZipCode { get; set; }
        [Display(Name = "Postort")]
        public string City { get; set; }
    }
}

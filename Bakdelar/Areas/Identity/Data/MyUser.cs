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
        public string FirstName { get; set; }
        [Required]
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
        [Required]
        public string Street { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string City { get; set; }
    }
}

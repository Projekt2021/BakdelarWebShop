using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DataModels
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        
        [Required, MaxLength(500), StringLength(500)]
        public string UserAddress { get; set; }

        [Required, MaxLength(20), StringLength(20)]
        public string UserPhoneNumber { get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

        //[ForeignKey("ApplicationUser")]
        //public string UserId { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }
    }
}

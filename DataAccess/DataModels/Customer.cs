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

        [MaxLength(500), StringLength(500)]
        public string UserAddress { get; set; }

        [MaxLength(100), StringLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100), StringLength(100)]
        public string LastName { get; set; }

        [MaxLength(100), StringLength(100)]
        public string PhoneNumber { get; set; }

        public ICollection<Cart> Carts { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

        public Guid UserId { get; set; }

        //[ForeignKey("ApplicationUser")]
        //public string UserId { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }
    }
}

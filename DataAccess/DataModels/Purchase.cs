using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DataModels
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseId { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
  
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }

        [Required, MaxLength(50), StringLength(50)]
        public string PurchaseStatus { get; set; }


        [ForeignKey("Product")]
        public int PrductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}

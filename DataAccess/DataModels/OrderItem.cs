using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DataModels
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemID { get; set; }

        [ForeignKey("Orders")]
        public int OrderID { get; set; }

        [ForeignKey("Products")]
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductPricePaidEach { get; set; }



        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductPricePaidTotal { get; set; }
        public int AmountOrdered { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.DataModels
{
    public class Order
    {
        public int OrderID { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string UserID { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeliveryDate { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCOAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerEmail { get; set; }
        public string PaymentMethod { get; set; }
        public bool ShippingPaid { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ShippingFee { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderCost { get; set; }


        public bool CouponUsed { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal CouponValue { get; set; }

        public bool HasBeenViewed { get; set; }

    }
}

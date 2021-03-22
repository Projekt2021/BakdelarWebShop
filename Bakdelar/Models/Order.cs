﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bakdelar.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerEmail { get; set; }
        public string PaymentMethod { get; set; }

        public bool ShippingPaid { get; set; }
        public decimal? ShippingFee { get; set; }

        public decimal OrderCost { get; set; }

    }
}
﻿using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01
{
    public class Order
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string CartId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int TotalQuantity { get; set; }
        public Decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }

        //navigational
        public virtual User User { get; set; } 
        public virtual Cart Cart { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual List<Payment> Payments { get; set; } = new List<Payment>();


    }
}

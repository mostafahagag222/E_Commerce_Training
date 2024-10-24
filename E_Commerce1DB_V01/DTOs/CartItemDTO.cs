using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.DTOs
{
    public class CartItemDTO
    {
        public decimal ProductPrice { get; set; }
        public decimal CartItemPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

    }
}

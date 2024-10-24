using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.DTOs
{
    public class AddOrderDTO
    {
        public List<CartItemDTO> Items { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}

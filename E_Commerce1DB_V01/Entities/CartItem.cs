using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01
{
    public class CartItem
    {
        public int Id { get; set; }
        public string CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity;

        //navigational
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}

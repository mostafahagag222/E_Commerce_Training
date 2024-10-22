using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01
{
    public class OrderItem

    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId {  get; set; }
        public int OrderId {  get; set; }
        public decimal OrderTotalPrice { get; set; }


        //navigational
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }


    }
}

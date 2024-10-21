using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Payloads
{
    public class AddToBasketPayload
    {
        public string Id { get; set; }
        public decimal ShippingPrice {  get; set; }
        public int DeliveryMethodId { get; set; }
        public List<ProductPayload> Items { get; set; }
    }
}

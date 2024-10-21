using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Payloads
{
    public class GetProductsPayload
    {
        public int? BrandID { get; set; }
        public int? TypeID { get; set; }
        public string Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; }
    }
}

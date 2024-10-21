using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //navigational
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}

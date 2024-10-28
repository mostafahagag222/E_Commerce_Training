using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Entities
{
    public class PaymentLog
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public string PaymentRequest { get; set; }
        public string PaymentResponse { get; set; }

        //navigational
        public virtual Payment Payment { get; set; }
    }
}

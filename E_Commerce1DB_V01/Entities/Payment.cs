using E_Commerce1DB_V01.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AdditionalDetails { get; set; }
        public string PaymentRequestUrl { get; set; }

        //navigational
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
        public virtual PaymentLog PaymentLog { get; set; }

    }
}

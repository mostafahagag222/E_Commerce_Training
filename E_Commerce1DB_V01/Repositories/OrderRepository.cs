using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ECPContext context;
        public OrderRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }
    }
}

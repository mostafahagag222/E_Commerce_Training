using E_Commerce1DB_V01.DTOs;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> GetOrderId(string cartId)
        {
            return await context.Orders.Where(o => o.CartId == cartId).Select(o => o.Id).FirstOrDefaultAsync();
        }
    }
}

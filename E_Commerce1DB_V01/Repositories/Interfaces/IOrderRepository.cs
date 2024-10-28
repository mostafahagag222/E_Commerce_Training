using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<int> GetOrderId(string cartId);
    }
}

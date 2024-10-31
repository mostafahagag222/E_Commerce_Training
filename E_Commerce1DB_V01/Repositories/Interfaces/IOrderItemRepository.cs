﻿using E_Commerce1DB_V01.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task TransferBasketItemsToOrderItemsAsync(string basketId);
    }
}

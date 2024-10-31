﻿using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Repositories;
using E_Commerce1DB_V01.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository  { get; }
        IBrandRepository BrandRepository  { get; }
        ITypeRepository TypeRepository { get; }
        IShippingMethodRepository ShippingMethodRepository { get; }
        IBasketItemRepository BasketItemRepository { get; }
        IBasketRepository BasketRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IPaymentLogRepository PaymentLogRepository { get; }

        //IPurchaseRepository PurchaseRepository { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

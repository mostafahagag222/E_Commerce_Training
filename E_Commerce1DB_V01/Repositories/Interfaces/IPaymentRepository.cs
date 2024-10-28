using E_Commerce1DB_V01.Entities;
using E_Commerce2Business_V01.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<HandlePaymentPaymentDataDTO> GetHandlePaymentPaymentDataDTO(string gUID);
        Task UpdateOrderId(int orderId, string gUID);
        Task UpdateStatusAsync(string gUID, PaymentStatus successfulPayment);
    }
}

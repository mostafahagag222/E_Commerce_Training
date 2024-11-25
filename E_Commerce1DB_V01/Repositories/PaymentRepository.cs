using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using E_Commerce1DB_V01.DTOs;

namespace E_Commerce1DB_V01.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly ECPContext context;
        public PaymentRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<HandlePaymentPaymentDataDTO> GetHandlePaymentPaymentDataDTO(string gUID)
        {
            var result = await context.Payments
                .Where(p => p.UniqueIdentifier == gUID)
                .Select(p => new HandlePaymentPaymentDataDTO 
                {
                    BasketId = p.BasketId,
                    PaymentId = p.Id,
                    UserId = p.UserId,
                    Amount = p.Amount
                })
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task UpdateOrderId(int orderId, string gUID)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(p => p.UniqueIdentifier == gUID);
            payment.OrderId = orderId;
        }

        public async Task UpdateStatusAsync(string gUID, PaymentStatus status)
        {
            var payment = await context.Payments.FirstOrDefaultAsync(p => p.UniqueIdentifier == gUID);
            payment.Status = status;
            payment.UpdatedAt = DateTime.Now;
            context.Payments.Update(payment);
        }

    }
}

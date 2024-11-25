using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Repositories.Interfaces;

namespace E_Commerce1DB_V01.Repositories
{
    public class PaymentLogRepository : GenericRepository<PaymentLog>, IPaymentLogRepository
    {
        private readonly ECPContext context;
        public PaymentLogRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }
    }
}

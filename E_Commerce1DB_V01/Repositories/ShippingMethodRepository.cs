using E_Commerce1DB_V01.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public class ShippingMethodRepository : GenericRepository<ShippingMethod>, IShippingMethodRepository
    {
        private readonly ECPContext context;

        public ShippingMethodRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<DeliveryMethodDTO>> GetDeliveryMethodsDTOAsync()
        {
            return context.ShippingMethods.Select(s => new DeliveryMethodDTO()
            {
                DeliveryTime = s.DeliveryTime,
                Description = s.Description,
                Id = int.Parse(s.Id),
                Price = s.Price,
                ShortName = s.Name
            }).ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly ECPContext context;

        public BrandRepository(ECPContext context):base(context)
        {
            this.context = context;
        }
        public async Task<bool> CheckExistenceByIDAsync(int? id)
        {
            return await context.Brands.AnyAsync(b => b.Id == id);
        }
    }
}

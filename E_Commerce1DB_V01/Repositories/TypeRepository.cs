using E_Commerce1DB_V01.DTOs;
using Microsoft.EntityFrameworkCore;
using E_Commerce1DB_V01.Repositories.Interfaces;
using Type = E_Commerce1DB_V01.Entities.Type;

namespace E_Commerce1DB_V01.Repositories
{
    public class TypeRepository : GenericRepository<Type>, ITypeRepository
    {
        private readonly ECPContext context;

        public TypeRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<List<GetTypesDTO>> GetTypesDTOAsync() => 
            await context.Types
            .AsNoTracking()
            .Select(b => new GetTypesDTO() { Id = b.Id, Name = b.Name })
            .ToListAsync();
        public async Task<bool> CheckExistenceByIDAsync(int? id) => 
            await context.Types
            .AnyAsync(t => t.Id == id);
    }
}

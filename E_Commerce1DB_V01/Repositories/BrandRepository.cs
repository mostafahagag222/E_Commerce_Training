﻿using E_Commerce1DB_V01.DTOs;
using Microsoft.EntityFrameworkCore;
using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Repositories.Interfaces;

namespace E_Commerce1DB_V01.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly ECPContext context;
        public ECPContext EcpContext => context;


        public BrandRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<bool> CheckExistenceByIDAsync(int? id) => 
            await context.Brands
            .AnyAsync(b => b.Id == id);
        public async Task<List<GetBrandsDTO>> GetBrandsDTOAsync() => 
            await context.Brands
            .AsNoTracking()
            .Select(b => new GetBrandsDTO() { Id = b.Id, Name = b.Name })
            .ToListAsync();
    }
}

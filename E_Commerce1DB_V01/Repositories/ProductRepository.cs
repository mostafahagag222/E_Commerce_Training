using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Repositories;
using E_Commerce2Business_V01.Payloads;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using E_Commerce1DB_V01.Extensions;


namespace E_Commerce1DB_V01.Entities
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ECPContext context;
        public ProductRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<PaginationDTO<GetProductsDTO>> GetProductsPage(GetProductsPayload payload)
        {
            #region MyRegion
            //// Preprocess sort option
            //if (!string.IsNullOrEmpty(payload.Sort))
            //{
            //    // Example input: "priceasc" or "pricedesc"
            //    if (payload.Sort.EndsWith("asc", StringComparison.OrdinalIgnoreCase))
            //    {
            //        payload.Sort = payload.Sort[0..^3].Trim() + " ascending"; // Remove "asc" and append "ascending"
            //    }
            //    else if (payload.Sort.EndsWith("desc", StringComparison.OrdinalIgnoreCase))
            //    {
            //        payload.Sort = payload.Sort[0..^4].Trim() + " descending"; // Remove "desc" and append "descending"
            //    }
            //} 
            #endregion
            var r = context.Products
            .AsNoTracking()
            .Where(p => !payload.BrandID.HasValue || p.BrandId == payload.BrandID)
            .Where(p => !payload.TypeID.HasValue || p.TypeId == payload.TypeID)
            .Where(p => string.IsNullOrEmpty(payload.Search) || p.Name.Contains(payload.Search));
            var sortOptions = (SortOptions)Enum.Parse(typeof(SortOptions), payload.Sort, true);
            switch (sortOptions)
            {
                case SortOptions.name:
                    r = r.OrderBy(p => p.Name);
                    break;
                case SortOptions.priceAsc:
                    r = r.OrderBy(p => p.Price);
                    break;
                case SortOptions.priceDesc:
                    r = r.OrderByDescending(p => p.Price);
                    break;
            }
            var rr = r.Select(p => new GetProductsDTO()
            {
                BrandId = p.BrandId,
                Description = p.Description,
                Id = p.Id,
                PictureUrl = p.ImageUrl,
                Name = p.Name,
                Price = p.Price,
                TypeId = p.TypeId,
                UnitsInStock = p.UnitsInStock
            });
            return await rr.ToPaginationAsync<GetProductsDTO>(payload.PageIndex, payload.PageSize);
            #region MyRegion
            //.OrderBy(payload.Sort.Trim().ToLower())
            //.Skip((payload.PageNumber - 1) * payload.PageSize)
            //.Take(payload.PageSize)
            //.Select(p => new GetProductsDTO()
            //{
            //    BrandId = p.BrandId,
            //    Description = p.Description,
            //    Id = p.Id,
            //    PictureUrl = p.ImageUrl,
            //    Name = p.Name,
            //    Price = p.Price,
            //    TypeId = p.TypeId,
            //    UnitsInStock = p.UnitsInStock
            //}).ToListAsync();
            //return new PaginationDTO<GetProductsDTO>()
            //{
            //    PageNumber = payload.PageNumber,
            //    PageSize = payload.PageSize,
            //    Count = r.Count(),
            //    Data = r
            //}; 
            #endregion

        }
        public async Task<List<GetBrandsDTO>> GetBrandsAsync()
        {
            return await context.Brands.AsNoTracking().Select(b => new GetBrandsDTO() { Id = b.Id, Name = b.Name }).ToListAsync();
        }
        public async Task<List<GetTypesDTO>> GetTypesAsync()
        {
            return await context.Types.AsNoTracking().Select(b => new GetTypesDTO() { Id = b.Id, Name = b.Name }).ToListAsync();
        }
        public async Task<List<ProductDTO>> GetProductsDetailsForCartItems(string cartId)
        {
            return await (from cartItem in context.CartItems
                          where cartItem.CartID == cartId
                          select cartItem
                    into cartItemsInSpecificCart
                          join p in context.Products
                          on cartItemsInSpecificCart.ProductID equals p.Id
                          select new ProductDTO()
                          {
                              Id = p.Id,
                              BrandId = p.BrandId,
                              Description = p.Description,
                              Name = p.Name,
                              PictureUrl = p.ImageUrl,
                              Price = p.Price,
                              TypeId = p.TypeId,
                              UnitsInStock = p.UnitsInStock
                          }).ToListAsync();
        }
        public async Task<int> GetUnitsInStockForOneProductAsync (int id)
        {
            return await (from p in context.Products
                          where p.Id == id
                          select p.UnitsInStock).FirstOrDefaultAsync();
        }
    }
}

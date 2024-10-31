using E_Commerce1DB_V01.DTOs;
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
        public async Task<PaginationDTO<GetProductsDTO>> GetProductsPageAsync(GetProductsPagePayload payload)
        {
            var allSelectedProducts = context.Products
            .AsNoTracking()
            .Where(p => !payload.BrandID.HasValue || p.BrandId == payload.BrandID)
            .Where(p => !payload.TypeID.HasValue || p.TypeId == payload.TypeID)
            .Where(p => string.IsNullOrEmpty(payload.Search) || p.Name.Contains(payload.Search));
            var sortOptions = (SortOptions)Enum.Parse(typeof(SortOptions), payload.Sort, true);
            switch (sortOptions)
            {
                case SortOptions.name:
                    allSelectedProducts = allSelectedProducts.OrderBy(p => p.Name);
                    break;
                case SortOptions.priceAsc:
                    allSelectedProducts = allSelectedProducts.OrderBy(p => p.Price);
                    break;
                case SortOptions.priceDesc:
                    allSelectedProducts = allSelectedProducts.OrderByDescending(p => p.Price);
                    break;
            }
            var allSelectedProductsDTO = allSelectedProducts.Select(p => new GetProductsDTO()
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
            var selectedProductsPage = await allSelectedProductsDTO.ToPaginationAsync<GetProductsDTO>(payload.PageIndex, payload.PageSize);
            return selectedProductsPage;
        }
        public async Task<int> GetUnitsInStockForOneProductAsync (int id)
        {
            return await (from p in context.Products
                          where p.Id == id
                          select p.UnitsInStock).FirstOrDefaultAsync();
        }
    }
}

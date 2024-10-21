using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Interfaces;
using E_Commerce2Business_V01.Payloads;

namespace E_Commerce2Business_V01.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<PaginationDTO<GetProductsDTO>> GetProductsPageAsync(GetProductsPayload payload)
        {
            if (payload.BrandID !=null && !await _unitOfWork.BrandRepository.CheckExistenceByIDAsync(payload.BrandID))
                throw new NotFoundException("invalid brand id");
            if (payload.TypeID !=null && !await _unitOfWork.TypeRepository.CheckExistenceByIDAsync(payload.TypeID))
                throw new NotFoundException("invalid type id");
            return await _unitOfWork.ProductRepository.GetProductsPage(payload);
        }
        public async Task<List<GetBrandsDTO>> GetBrands()
        {
            return await _unitOfWork.ProductRepository.GetBrandsAsync();
        }
        public async Task<List<GetTypesDTO>> GetTypes()
        {
            return await _unitOfWork.ProductRepository.GetTypesAsync();
        }
        public async Task<List<ProductDTO>> GetProductsDetailsForCartItems(string cartId)
        {
            return await _unitOfWork.ProductRepository.GetProductsDetailsForCartItems(cartId);
        }
        public async Task<int> GetUnitsInStockForOneProductAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetUnitsInStockForOneProductAsync(id);
        }

    }
}

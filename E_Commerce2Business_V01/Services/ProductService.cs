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
        public async Task<PaginationDTO<GetProductsDTO>> GetProductsPageAsync(GetProductsPagePayload payload)
        {
            if (payload.BrandID !=null && !await _unitOfWork.BrandRepository.CheckExistenceByIDAsync(payload.BrandID))
                throw new NotFoundException("invalid brand id");
            if (payload.TypeID !=null && !await _unitOfWork.TypeRepository.CheckExistenceByIDAsync(payload.TypeID))
                throw new NotFoundException("invalid type id");
            return await _unitOfWork.ProductRepository.GetProductsPageAsync(payload);
        }
    }
}

using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Interfaces;

namespace E_Commerce2Business_V01.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<GetBrandsDTO>> GetBrandsDTOAsync() => await _unitOfWork.BrandRepository.GetBrandsDTOAsync();
    }
}

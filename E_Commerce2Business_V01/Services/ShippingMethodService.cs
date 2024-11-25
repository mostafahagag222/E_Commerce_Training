using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Interfaces;

namespace E_Commerce2Business_V01.Services
{
    public class ShippingMethodService : IShippingMethodService

    {
        private readonly IUnitOfWork _unitOfWork;

        public ShippingMethodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DeliveryMethodDTO>> GetDeliveryMethodsDTOAsync()
        {
            return await _unitOfWork.ShippingMethodRepository.GetDeliveryMethodsDTOAsync();
        }
    }
}

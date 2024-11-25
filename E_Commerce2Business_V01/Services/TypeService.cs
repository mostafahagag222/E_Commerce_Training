using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Interfaces;

namespace E_Commerce2Business_V01.Services
{
    public class TypeService : ITypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<GetTypesDTO>> GetTypesDTOAsync() => await _unitOfWork.TypeRepository.GetTypesDTOAsync();
    }
}

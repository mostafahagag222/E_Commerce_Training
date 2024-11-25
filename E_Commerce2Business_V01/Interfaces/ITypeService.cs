using E_Commerce1DB_V01.DTOs;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface ITypeService
    {
        Task<List<GetTypesDTO>> GetTypesDTOAsync();
    }
}

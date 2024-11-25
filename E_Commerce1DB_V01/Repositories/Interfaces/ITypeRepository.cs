using E_Commerce1DB_V01.DTOs;

namespace E_Commerce1DB_V01.Repositories.Interfaces
{
    public interface ITypeRepository
    {
        Task<bool> CheckExistenceByIDAsync(int? id);
        Task<List<GetTypesDTO>> GetTypesDTOAsync();
    }
}

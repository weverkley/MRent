using MRent.Application.DTO;

namespace MRent.Application.Interfaces
{
    public interface IMotorcycleService
    {
        Task<IEnumerable<MotorcycleDTO>> GetAllAsync();
        Task<MotorcycleDTO> GetByIdAsync(Guid id);
        Task<MotorcycleDTO> GetByIdentifierAsync(string identifier);
        Task<MotorcycleDTO> GetByPlateAsync(string plate);
        Task CreateAsync(MotorcycleDTO entity);
        Task UpdateAsync(MotorcycleDTO entity);
        Task DeleteAsync(MotorcycleDTO entity);
    }
}

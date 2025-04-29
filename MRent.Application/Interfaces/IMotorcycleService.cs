using MRent.Application.DTO;

namespace MRent.Application.Interfaces
{
    public interface IMotorcycleService
    {
        Task<IEnumerable<MotorcycleDTO>> GetAllAsync();
        Task<MotorcycleDTO> GetByIdAsync(Guid id);
        Task<MotorcycleDTO> GetByPlateAsync(string plate);
        Task CreateAsync(MotorcycleDTO entity);
        Task UpdatePlateAsync(Guid id, string plate);
        Task DeleteAsync(Guid id);
    }
}

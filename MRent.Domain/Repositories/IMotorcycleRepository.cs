using MRent.Domain.Entities;

namespace MRent.Domain.Repositories
{
    public interface IMotorcycleRepository : IBaseRepository<MotorcycleEntity>
    {
        Task<MotorcycleEntity?> GetByIdentifierAsync(string identifier);
        Task<MotorcycleEntity?> GetByPlateAsync(string identifier);
        Task<bool> IsPlateUnique(string plate);
    }
}

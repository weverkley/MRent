using MRent.Domain.Entities;

namespace MRent.Domain.Repositories
{
    public interface IMotorcycleRepository : IBaseRepository<MotorcycleEntity>
    {
        Task<MotorcycleEntity?> GetByPlateAsync(string identifier);
        Task<bool> IsPlateUniqueAsync(string plate);
    }
}

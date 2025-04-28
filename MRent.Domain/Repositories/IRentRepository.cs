using MRent.Domain.Entities;

namespace MRent.Domain.Repositories
{
    public interface IRentRepository : IBaseRepository<RentEntity>
    {
        Task<IEnumerable<RentEntity>> GetByMotorcycleIdAsync(Guid motorcycleId);
    }
}

using MRent.Domain.Entities;

namespace MRent.Domain.Repositories
{
    public interface IPlanRepository : IBaseRepository<PlanEntity>
    {
        Task<IEnumerable<PlanEntity>> GetByDaysAsync(int days);
    }
}

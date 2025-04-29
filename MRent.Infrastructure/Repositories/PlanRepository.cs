using Microsoft.EntityFrameworkCore;
using MRent.Domain.Entities;
using MRent.Domain.Repositories;
using MRent.Infrastructure.Contexts;

namespace MRent.Infrastructure.Repositories
{
    public class PlanRepository : BaseRepository<PlanEntity>, IPlanRepository
    {
        public PlanRepository(PostgresContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PlanEntity>> GetByDaysAsync(int days)
        {
            return await _context.Plans
                .Where(p => p.Days == days)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}


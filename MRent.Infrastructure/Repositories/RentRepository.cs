using Microsoft.EntityFrameworkCore;
using MRent.Domain.Entities;
using MRent.Domain.Repositories;
using MRent.Infrastructure.Contexts;

namespace MRent.Infrastructure.Repositories
{
    public class RentRepository : BaseRepository<RentEntity>, IRentRepository
    {
        public RentRepository(PostgresContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RentEntity>> GetByMotorcycleIdAsync(Guid motorcycleId)
        {
            return await _context.Rents
                .AsNoTracking()
                .Where(m => m.MotorcycleId == motorcycleId)
                .ToListAsync();
        }
    }
}


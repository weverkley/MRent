using Microsoft.EntityFrameworkCore;
using MRent.Domain.Entities;
using MRent.Domain.Repositories;
using MRent.Infrastructure.Contexts;

namespace MRent.Infrastructure.Repositories
{
    public class MotorcicleRepository : BaseRepository<MotorcycleEntity>, IMotorcycleRepository
    {
        public MotorcicleRepository(PostgresContext context) : base(context)
        {
        }

        public async Task<MotorcycleEntity?> GetByPlateAsync(string plate)
        {
            return await _context.Motorcycles
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Plate == plate);
        }

        public async Task<MotorcycleEntity?> GetByIdentifierAsync(string identifier)
        {
            return await _context.Motorcycles
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Identifier == identifier);
        }

        public async Task<bool> IsPlateUniqueAsync(string plate)
        {
            var entity = await _context.Motorcycles
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Plate == plate);
            return entity == null;
        }
    }
}


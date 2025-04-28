using MRent.Domain.Entities;
using MRent.Domain.Repositories;
using MRent.Infrastructure.Contexts;

namespace MRent.Infrastructure.Repositories
{
    public class MotorcicleLogRepository : BaseRepository<MotorcycleLogEntity>, IMotorcycleLogRepository
    {
        public MotorcicleLogRepository(PostgresContext context) : base(context)
        {
        }
    }
}


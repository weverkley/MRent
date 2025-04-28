using MRent.Domain.Entities;

namespace MRent.Domain.Repositories
{
    public interface ICourierRepository : IBaseRepository<CourierEntity>
    {
        Task<bool> IsCNPJUniqueAsync(string cnpj);
        Task<bool> IsCNHUniqueAsync(string cnpj);
    }
}

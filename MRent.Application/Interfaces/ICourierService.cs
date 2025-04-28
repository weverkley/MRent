using MRent.Application.DTO;

namespace MRent.Application.Interfaces
{
    public interface ICourierService
    {
        Task CreateAsync(CourierDTO entity);
    }
}

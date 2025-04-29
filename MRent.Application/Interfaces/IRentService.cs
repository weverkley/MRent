using MRent.Application.DTO;

namespace MRent.Application.Interfaces
{
    public interface IRentService
    {
        Task CreateAsync(RentDTO entity);
        Task<RentDTO> GetByIdAsync(Guid id);
        Task UpdateReturnDateAsync(Guid id, DateTime returnDate);
    }
}

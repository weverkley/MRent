using Microsoft.AspNetCore.Http;
using MRent.Application.DTO;

namespace MRent.Application.Interfaces
{
    public interface ICourierService
    {
        Task CreateAsync(CourierDTO entity);
        Task UpdateCnhImageAsync(Guid id, IFormFile image);
    }
}

using AutoMapper;
using MRent.Application.Commands.Motorcycle;
using MRent.Domain.Entities;

namespace MRent.Application.AutoMapper.Profiles
{
    public class CommandToEntity : Profile
    {
        public CommandToEntity()
        {
            CreateMap<CreateMotorcycleCommand, MotorcycleEntity>()
                .ReverseMap();
        }
    }
}

using AutoMapper;
using MRent.Application.Commands.Courier;
using MRent.Application.Commands.Motorcycle;
using MRent.Application.Commands.Rent;
using MRent.Domain.Entities;

namespace MRent.Application.AutoMapper.Profiles
{
    public class CommandToEntity : Profile
    {
        public CommandToEntity()
        {
            CreateMap<CreateMotorcycleCommand, MotorcycleEntity>()
                .ReverseMap();

            CreateMap<CreateCourierCommand, CourierEntity>()
                .ReverseMap();

            CreateMap<CreateRentCommand, RentEntity>()
                .ReverseMap();

            CreateMap<UpdateRentCommand, RentEntity>()
                .ReverseMap();
        }
    }
}

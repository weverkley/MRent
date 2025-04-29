using AutoMapper;
using MRent.Application.Commands.Rent;
using MRent.Application.DTO;

namespace MRent.Application.AutoMapper.Profiles
{
    public class DTOToCommand : Profile
    {
        public DTOToCommand()
        {
            CreateMap<RentDTO, CreateRentCommand>()
                .ForMember(t => t.MotorcycleId, m => m.MapFrom(s => s.moto_id))
                .ForMember(t => t.CourierId, m => m.MapFrom(s => s.entregador_id))
                .ForMember(t => t.StartDate, m => m.MapFrom(s => s.data_inicio))
                .ForMember(t => t.EndDate, m => m.MapFrom(s => s.data_termino))
                .ForMember(t => t.ExpectedEndDate, m => m.MapFrom(s => s.data_previsao_termino))
                .ReverseMap();
        }
    }
}

using AutoMapper;
using MRent.Application.DTO;
using MRent.Domain.Entities;
using MRent.Domain.Enums;

namespace MRent.Application.AutoMapper.Profiles
{
    public class EntityToDTO : Profile
    {
        public EntityToDTO()
        {
            CreateMap<MotorcycleEntity, MotorcycleDTO>()
                .ForMember(t => t.identificador, m => m.MapFrom(s => s.Identifier))
                .ForMember(t => t.ano, m => m.MapFrom(s => s.Year))
                .ForMember(t => t.modelo, m => m.MapFrom(s => s.Model))
                .ForMember(t => t.placa, m => m.MapFrom(s => s.Plate));

            CreateMap<CourierEntity, CourierDTO>()
                .ForMember(t => t.identificador, m => m.MapFrom(s => s.Identifier))
                .ForMember(t => t.nome, m => m.MapFrom(s => s.Name))
                .ForMember(t => t.cnpj, m => m.MapFrom(s => s.CNPJ))
                .ForMember(t => t.data_nascimento, m => m.MapFrom(s => s.BornDate))
                .ForMember(t => t.numero_cnh, m => m.MapFrom(s => s.CNH))
                .ForMember(t => t.tipo_cnh, m => m.MapFrom(s => _GetCNHType(s.CNHType)))
                .ForMember(t => t.imagem_cnh, m => m.MapFrom(s => s.CNHImage));
        }

        private string _GetCNHType(ECNHType value)
        {
            if (value == ECNHType.A)
                return "A";
            else if (value == ECNHType.B)
                return "B";
            else if (value == ECNHType.AB)
                return "A+B";
            else
                return "N/A";
        }
    }
}

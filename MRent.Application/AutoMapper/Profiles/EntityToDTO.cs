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
                .ForMember(t => t.identificador, m => m.MapFrom(s => s.Id))
                .ForMember(t => t.ano, m => m.MapFrom(s => s.Year))
                .ForMember(t => t.modelo, m => m.MapFrom(s => s.Model))
                .ForMember(t => t.placa, m => m.MapFrom(s => s.Plate));

            CreateMap<CourierEntity, CourierDTO>()
                .ForMember(t => t.identificador, m => m.MapFrom(s => s.Id))
                .ForMember(t => t.nome, m => m.MapFrom(s => s.Name))
                .ForMember(t => t.cnpj, m => m.MapFrom(s => s.CNPJ))
                .ForMember(t => t.data_nascimento, m => m.MapFrom(s => s.BornDate))
                .ForMember(t => t.numero_cnh, m => m.MapFrom(s => s.CNH))
                .ForMember(t => t.tipo_cnh, m => m.MapFrom(s => _GetCNHType(s.CNHType)))
                .ForMember(t => t.imagem_cnh, m => m.MapFrom(s => s.CNHImage));

            CreateMap<RentEntity, RentDTO>()
                .ForMember(t => t.identificador, m => m.MapFrom(s => s.Id))
                .ForMember(t => t.moto_id, m => m.MapFrom(s => s.MotorcycleId))
                .ForMember(t => t.entregador_id, m => m.MapFrom(s => s.CourierId))
                .ForMember(t => t.valor_diaria, m => m.AllowNull())
                .ForMember(t => t.data_devolucao, m => m.AllowNull())
                .ForMember(t => t.data_inicio, m => m.MapFrom(s => s.StartDate))
                .ForMember(t => t.data_termino, m => m.MapFrom(s => s.EndDate))
                .ForMember(t => t.data_previsao_termino, m => m.MapFrom(s => s.ExpectedEndDate))
                .ForMember(t => t.multa, m => m.MapFrom(s => s.Tax))
                .ForMember(t => t.subtotal, m => m.MapFrom(s => s.Subtotal))
                .ForMember(t => t.total, m => m.MapFrom(s => s.Total))
                .ForMember(t => t.plano, m => m.AllowNull())
                .AfterMap((s, t) =>
                {
                    t.valor_diaria = s.Plan.DailyValue;
                    t.data_devolucao = s.EndDate;
                    t.plano = s.Plan.Days;
                });
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

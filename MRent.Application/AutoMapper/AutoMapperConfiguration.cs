using AutoMapper;
using MRent.Application.AutoMapper.Profiles;

namespace MRent.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration ConfigureMappings()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToDTO());
                cfg.AddProfile(new CommandToEntity());
                cfg.AddProfile(new DTOToCommand());
            });
            return mapperConfiguration;
        }
    }
}

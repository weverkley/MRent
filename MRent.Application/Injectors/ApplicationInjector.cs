using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MRent.Application.AutoMapper;
using MRent.Application.Commands.Motorcycle;
using MRent.Application.Interfaces;
using MRent.Application.Services;

namespace MRent.Application.Injectors
{
    public static class ApplicationInjector
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var mapper = AutoMapperConfiguration.ConfigureMappings();
            services.AddAutoMapper(x => mapper.CreateMapper(), Assembly.Load("MRent.Application"));

            // FluentValidation
            services.AddScoped<IValidator<CreateMotorcycleCommand>, CreateMotorcycleCommandValidator>();
            services.AddScoped<IValidator<UpdateMotorcycleCommand>, UpdateMotorcycleCommandValidator>();
            services.AddScoped<IValidator<DeleteMotorcycleCommand>, DeleteMotorcycleCommandValidator>();

            // Application Services
            services.AddTransient<IMotorcycleService, MotorcycleService>();

            return services;
        }
    }
}

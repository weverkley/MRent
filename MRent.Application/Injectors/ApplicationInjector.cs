using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using MRent.Application.AutoMapper;
using MRent.Application.Commands.Courier;
using MRent.Application.Commands.Motorcycle;
using MRent.Application.Interfaces;
using MRent.Application.Services;

namespace MRent.Application.Injectors
{
    public static class ApplicationInjector
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mapper = AutoMapperConfiguration.ConfigureMappings();
            services.AddAutoMapper(x => mapper.CreateMapper(), Assembly.Load("MRent.Application"));

            services.AddMinio(configureClient => configureClient
                .WithEndpoint(configuration["Minio:Endpoint"])
                .WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
                .WithSSL(bool.Parse(configuration["Minio:UseSSL"]))
                .Build());

            // FluentValidation
            services.AddScoped<IValidator<CreateMotorcycleCommand>, CreateMotorcycleCommandValidator>();
            services.AddScoped<IValidator<UpdateMotorcyclePlateCommand>, UpdateMotorcyclePlateCommandValidator>();
            services.AddScoped<IValidator<CreateCourierCommand>, CreateCourierCommandValidator>();
            services.AddScoped<IValidator<UpdateCourierImageCNHCommand>, UpdateCourierImageCNHCommandValidator>();

            // Application Services
            services.AddTransient<IMotorcycleService, MotorcycleService>();
            services.AddTransient<ICourierService, CourierService>();
            services.AddTransient<IMinioService, MinioService>();

            return services;
        }
    }
}

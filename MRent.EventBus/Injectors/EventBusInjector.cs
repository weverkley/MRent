using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MRent.EventBus.Interfaces;
using MRent.EventBus.MassTransit;

namespace MRent.EventBus.Injectors
{
    public static class EventBusInjector
    {
        public static IServiceCollection RegisterMassTransitAsEventBus(this IServiceCollection services, IConfiguration configuration, bool isScoped = false, bool isHostedService = false)
        {
            return RegisterMassTransitAsEventBus<MassTransitStartup>(services, configuration, isScoped, isHostedService);
        }

        public static IServiceCollection RegisterMassTransitAsEventBus<TStartup>(this IServiceCollection services, IConfiguration configuration, bool isScoped = false, bool isHostedService = false)
            where TStartup : class, IEventBusStartup
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumers(Assembly.Load("MRent.Application"));

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["MassTransit:RabbitMQ:Host"], h =>
                    {
                        h.Username(configuration["MassTransit:RabbitMQ:Username"]);
                        h.Password(configuration["MassTransit:RabbitMQ:Password"]);
                    });
                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(true));
                });
            });


            RegisterEventBus<TStartup>(services, isScoped);

            if (isHostedService) services.AddHostedService<MassTransitStartup>();

            return services;
        }

        public static IServiceCollection RegisterInMemoryAsEventBus(this IServiceCollection services, bool isScoped = false)
        {
            services.AddMassTransit(x =>
            {
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(true));
                });
            });

            RegisterEventBus<MassTransitStartup>(services, isScoped);
            return services;
        }

        private static void RegisterEventBus<TStartup>(IServiceCollection services, bool isScoped = false)
            where TStartup : class, IEventBusStartup
        {
            if (isScoped)
            {
                services.AddScoped<IEventBus, MassTransitEventBus>();
                services.AddSingleton<IEventBusStartup, TStartup>();
            }
            else
            {
                services.AddSingleton<IEventBus, MassTransitEventBus>();
                services.AddSingleton<IEventBusStartup, TStartup>();
            }

        }
    }
}
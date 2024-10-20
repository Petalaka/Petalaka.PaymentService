using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;


namespace Petalaka.Payment.Service
{
    public static class ConfigureService
    {
        public static void AddConfigureServiceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjectionService(configuration);
            services.AddMasstransitRabbitmq(configuration);
        }

        public static void AddMasstransitRabbitmq(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                //config rabbitmq host
                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMq:Host"], ushort.Parse(configuration["RabbitMq:Port"]),
                        configuration["RabbitMq:VirtualHost"], h =>
                        {
                            h.Username(configuration["RabbitMq:Username"]);
                            h.Password(configuration["RabbitMq:Password"]);
                        });
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddMassTransitHostedService();
        }
    }
}

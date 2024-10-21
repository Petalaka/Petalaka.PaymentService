using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Settings;


namespace Petalaka.Payment.Service
{
    public static class ConfigureService
    {
        public static void AddConfigureServiceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjectionService(configuration);
            services.AddMasstransitRabbitmq(configuration);
            services.AddZaloPayConfig(configuration);
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

        public static void AddZaloPayConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var zaloPayConfig = configuration.GetSection("ZaloPay");
            services.AddSingleton<ZaloPaySettings>(options =>
            {
                var zaloPaySettings = new ZaloPaySettings
                {
                    AppId = zaloPayConfig.GetSection("AppId").Value,
                    Key1 = zaloPayConfig.GetSection("Key1").Value,
                    Key2 = zaloPayConfig.GetSection("Key2").Value,
                    CallbackUrl = zaloPayConfig.GetSection("CallbackUrl").Value
                };
                zaloPaySettings.IsValid();
                return zaloPaySettings;
            });
        }
    }
}

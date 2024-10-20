using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Petalaka.Payment.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Payment.Repository
{
    public static class ConfigureService
    {
        public static async void AddConfigureServiceRepository(this IServiceCollection services, IConfiguration configuration)
        {
            /*services.ConfigSwagger();
            services.AddAuthenJwt(configuration);
            services.AddDatabase(configuration);
            services.AddServices();
            services.ConfigRoute();
            services.AddInitialiseDatabase();
            services.ConfigCors();
            //services.ConfigCorsSignalR();
            //services.RabbitMQConfig(configuration);
            services.JwtSettingsConfig(configuration);*/
            services.AddDatabase(configuration);
            services.AddDependencyInjectionRepository(configuration);
            //services.AddAutoMapperConfig(configuration);
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PetalakaDbContext>(options =>
            {
                string? connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString ??
                                     throw new InvalidOperationException(
                                         "Connection string not found in appsettings.json"),
                                         options => options.EnableRetryOnFailure()
                                         ).UseLazyLoadingProxies();
            });
        }



        public static async Task UseInitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
            await dbInitializer.InitializeAsync();
        }
    }
}

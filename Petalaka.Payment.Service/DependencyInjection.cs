using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petalaka.Payment.Service.Interface;
using Petalaka.Payment.Service.Services;

namespace Petalaka.Payment.Service
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjectionService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPaymentService, PaymentService>();
        }
    }
}

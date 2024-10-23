using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petalaka.Payment.Service.Interface;
using Petalaka.Payment.Service.Services;
using Petalaka.Payment.Service.ThirdParties.ZaloPay;
using Petalaka.Payment.Service.ThirdParties.ZaloPay.Interfaces;

namespace Petalaka.Payment.Service
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjectionService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IZaloPayService, ZaloPayService>();
            services.AddScoped<IPaymentMethod, ZaloPayPayment>();
            services.AddScoped<ZaloPayPayment>();
            services.AddScoped<PaymentMethodFactory>();
            services.AddScoped<IPaymentGatewayService, PaymentGatewayService>();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Petalaka.Payment.Core.CustomException;
using Petalaka.Payment.Repository.Entities;
using Petalaka.Payment.Repository.Interface;
using Petalaka.Payment.Service.Interface;

namespace Petalaka.Payment.Service.Services;

public class PaymentMethodFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IUnitOfWork _unitOfWork;
    public PaymentMethodFactory(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
    {
        _serviceProvider = serviceProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IPaymentMethod> GetPaymentMethod(string paymentGatewayId)
    {
        var paymentGateway = await _unitOfWork.GetRepository<PaymentGateway>().FindAsync(p => p.Id.ToString() == paymentGatewayId);
        if (paymentGateway == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Payment gateway not found or not support");
        }

        IPaymentMethod paymentMethod;
        switch (paymentGateway.Name)
        {
            case "ZALOPAY":
                paymentMethod = _serviceProvider.GetRequiredService<ZaloPayPayment>();
                break;
            default:
                throw new CoreException(StatusCodes.Status400BadRequest, "Payment method not found");
        }

        return paymentMethod;
    }
}
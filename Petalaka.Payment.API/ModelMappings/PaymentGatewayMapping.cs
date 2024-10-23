using AutoMapper;
using Petalaka.Payment.API.ModelViews.RequestModels.PaymentGatewayRequest;
using Petalaka.Payment.API.ModelViews.ResponseModels.PaymentGatewayResponse;
using Petalaka.Payment.Repository.Entities;
using Petalaka.Payment.Service.BusinessModels;

namespace Petalaka.Payment.API.ModelMappings;

public class PaymentGatewayMapping : Profile
{
    public PaymentGatewayMapping()
    {
        CreateMap<PaymentGateway, PaymentGatewayBusinessModel>().ReverseMap();

        CreateMap<CreatePaymentGatewayRequest, PaymentGatewayBusinessModel>();
        
        CreateMap<PaymentGatewayBusinessModel, CreatePaymentGatewayResponse>().ReverseMap();
        CreateMap<PaymentGatewayBusinessModel, GetPaymentGatewayResponse>().ReverseMap();
    }
}
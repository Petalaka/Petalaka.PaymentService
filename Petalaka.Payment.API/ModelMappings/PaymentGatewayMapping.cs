﻿using AutoMapper;
using Petalaka.Payment.API.ModelViews.RequestModels.PaymentGatewayRequest;
using Petalaka.Payment.API.ModelViews.ResponseModels.OrderResponse;
using Petalaka.Payment.API.ModelViews.ResponseModels.PaymentGatewayResponse;
using Petalaka.Payment.API.ModelViews.ResponseModels.PaymentResponse;
using Petalaka.Payment.Repository.Entities;
using Petalaka.Payment.Service.BusinessModels;

namespace Petalaka.Payment.API.ModelMappings;

public class PaymentGatewayMapping : Profile
{
    public PaymentGatewayMapping()
    {
        CreateMap<PaymentGateway, PaymentGatewayBusinessModel>()
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdatedTime))
            .ForMember(dest => dest.DeleteTime, opt => opt.MapFrom(src => src.DeletedTime))
            .ForMember(dest => dest.CreateBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
            .ForMember(dest => dest.DeleteBy, opt => opt.MapFrom(src => src.DeletedBy));

        CreateMap<PaymentGatewayBusinessModel, PaymentGateway>();

        CreateMap<CreatePaymentGatewayRequest, PaymentGatewayBusinessModel>();
        
        CreateMap<PaymentGatewayBusinessModel, CreatePaymentGatewayResponse>().ReverseMap();
        CreateMap<PaymentGatewayBusinessModel, GetPaymentGatewayResponse>().ReverseMap();

        CreateMap<GetPaymentResponse, GetUserOrderResponse>().ReverseMap();
    }
}
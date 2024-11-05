using AutoMapper;
using Petalaka.Payment.API.ModelViews.ResponseModels.OrderDetailAdditionalDetailResponse;
using Petalaka.Payment.Repository.Entities;
using Petalaka.Payment.Service.BusinessModels;

namespace Petalaka.Payment.API.ModelMappings;

public class OrderDetailAdditionalDetailMapping : Profile
{
    public OrderDetailAdditionalDetailMapping()
    {
        CreateMap<OrderDetailAdditionalDetail, OrderDetailAdditionalDetailBusinessModel>()
            .ForMember(dest => dest.CreateTime, opt => opt.MapFrom(src => src.CreatedTime))
            .ForMember(dest => dest.LastUpdateTime, opt => opt.MapFrom(src => src.LastUpdatedTime))
            .ForMember(dest => dest.DeleteTime, opt => opt.MapFrom(src => src.DeletedTime))
            .ForMember(dest => dest.CreateBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.LastUpdateBy, opt => opt.MapFrom(src => src.LastUpdatedBy))
            .ForMember(dest => dest.DeleteBy, opt => opt.MapFrom(src => src.DeletedBy));
        
        CreateMap<OrderDetailAdditionalDetailBusinessModel, OrderDetailAdditionalDetail>().ReverseMap();

        CreateMap<OrderDetailAdditionalDetailBusinessModel, GetOrderDetailAdditionalDetailResponse>().ReverseMap();
    }
}
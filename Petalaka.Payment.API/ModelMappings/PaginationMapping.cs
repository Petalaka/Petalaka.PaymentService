using AutoMapper;
using Petalaka.Payment.Repository.Base;

namespace Petalaka.Payment.API.ModelMappings;

public class PaginationMapping : Profile
{
    public PaginationMapping()
    {
        CreateMap(typeof(PaginationResponse<>), typeof(BaseResponsePagination<>));
    }
}
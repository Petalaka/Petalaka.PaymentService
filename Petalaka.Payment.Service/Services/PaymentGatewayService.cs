using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Petalaka.Payment.Core.CustomException;
using Petalaka.Payment.Core.Utils;
using Petalaka.Payment.Repository.Base;
using Petalaka.Payment.Repository.Entities;
using Petalaka.Payment.Repository.Interface;
using Petalaka.Payment.Service.BusinessModels;
using Petalaka.Payment.Service.Interface;
using Petalaka.Payment.Service.QueryOptions.FilterOptions.PaymentFilters;
using Petalaka.Payment.Service.QueryOptions.FilterOptions.PaymentGatewayFilters;
using Petalaka.Payment.Service.QueryOptions.RequestOptions;
using Petalaka.Payment.Service.QueryOptions.SortOptions.PaymentGatewaySorts;
using Petalaka.Payment.Service.QueryOptions.SortOptions.PaymentSorts;

namespace Petalaka.Payment.Service.Services;

public class PaymentGatewayService : IPaymentGatewayService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PaymentGatewayService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<PaginationResponse<PaymentGatewayBusinessModel>>
        GetPaymentGateways(RequestOptionsBase<GetAllPaymentGatewayFilterOptions, GetAllPaymentGatewaySortOptions> request)
    {
        var paymentGatewayQuery = _unitOfWork.GetRepository<PaymentGateway>().AsQueryable();

        if (request.IsDelete == true)
        {
            paymentGatewayQuery = paymentGatewayQuery.Where(x => x.DeletedTime != null);
        }
        else
        {
            paymentGatewayQuery = paymentGatewayQuery.Where(x => x.DeletedTime == null);
        }

        if (request.FilterOptions == null)
        {
            paymentGatewayQuery = paymentGatewayQuery.Where(p => p.IsSuspended == false);
        }
        else
        {
            if (!String.IsNullOrWhiteSpace(request.FilterOptions.Name))
            {
                paymentGatewayQuery = paymentGatewayQuery.Where(p => p.Name.Contains(request.FilterOptions.Name));
            }

            if (!String.IsNullOrWhiteSpace(request.FilterOptions.RegionCode))
            {
                paymentGatewayQuery = paymentGatewayQuery.Where(p => p.RegionCode == request.FilterOptions.RegionCode);
            }
            
            if (request.FilterOptions.IsSuspended != null)
            {
                paymentGatewayQuery = paymentGatewayQuery.Where(p => p.IsSuspended == request.FilterOptions.IsSuspended);
            }

            if (request.FilterOptions.CreatedTimeRange != null)
            {
                paymentGatewayQuery = paymentGatewayQuery.Where(p => p.CreatedTime >= request.FilterOptions.CreatedTimeRange.From 
                                                                     && p.CreatedTime <= request.FilterOptions.CreatedTimeRange.To);
            }
            
        }

        switch (request.SortOptions)
        {
            case GetAllPaymentGatewaySortOptions.NameAlphabeticalAz:
                paymentGatewayQuery = paymentGatewayQuery.OrderBy(p => p.Name);
                break;
            case GetAllPaymentGatewaySortOptions.NameAlphabeticalZa:
                paymentGatewayQuery = paymentGatewayQuery.OrderByDescending(p => p.Name);
                break;
            case GetAllPaymentGatewaySortOptions.CreatedTimeAscending:
                paymentGatewayQuery = paymentGatewayQuery.OrderBy(p => p.CreatedTime);
                break;
            case GetAllPaymentGatewaySortOptions.CreatedTimeDescending:
                paymentGatewayQuery = paymentGatewayQuery.OrderByDescending(p => p.CreatedTime);
                break;
            default:
                paymentGatewayQuery = paymentGatewayQuery.OrderBy(p => p.CreatedTime);
                break;
        }

        var queryPagination = await _unitOfWork.GetRepository<PaymentGateway>()
            .GetPagination(paymentGatewayQuery, request.PageNumber, request.PageSize);
        
        var paymentGateways = _mapper.Map<List<PaymentGatewayBusinessModel>>(queryPagination.Data);
        return new PaginationResponse<PaymentGatewayBusinessModel>(paymentGateways, queryPagination.PageNumber,
            queryPagination.PageSize, queryPagination.TotalRecords, queryPagination.CurrentPageRecords);
    }

    public async Task<PaymentGatewayBusinessModel> CreatePaymentGateway(PaymentGatewayBusinessModel paymentGatewayBusinessModel)
    {
        var exitstingPaymentGateway = await _unitOfWork.GetRepository<PaymentGateway>()
            .AsQueryable()
            .FirstOrDefaultAsync(x => x.Name == paymentGatewayBusinessModel.Name);
        if (exitstingPaymentGateway != null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Payment gateway with this name is already exists");
        }
        var paymentGateway = _mapper.Map<PaymentGateway>(paymentGatewayBusinessModel);
        paymentGateway.Name = StringConverterHelper.CapitalizeString(paymentGateway.Name);
        if (paymentGateway.RegionCode != null)
            paymentGateway.RegionCode = StringConverterHelper.CapitalizeString(paymentGateway.RegionCode);
        await _unitOfWork.GetRepository<PaymentGateway>().InsertAsync(paymentGateway);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<PaymentGatewayBusinessModel>(paymentGateway);
    }
}
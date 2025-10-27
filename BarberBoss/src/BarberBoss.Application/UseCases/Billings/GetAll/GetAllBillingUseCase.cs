using BarberBoss.Application.Common.Validation;
using BarberBoss.Application.Mappers;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.UseCases.Billings.GetAll;
internal class GetAllBillingUseCase : IGetAllBillingUseCase
{
    private readonly IBillingReadOnlyRepository _billingReadOnlyRepository;
    public GetAllBillingUseCase(IBillingReadOnlyRepository billingReadOnlyRepository)
    {
        _billingReadOnlyRepository = billingReadOnlyRepository;
    }
    public async Task<ResponseBillingsJson> Execute(RequestBillingsJson request)
    {
        ValidateRequest.ValidatePagination(request);
        var (billings, totalBillings) = await _billingReadOnlyRepository.GetAll(request.Page, request.PageSize);
        var result = billings.ToResponseBillingsJson(request.Page, request.PageSize, totalBillings);
        return result;
    }

    private ResponseBillingsJson SetupResponseBillingsJson(ResponseBillingsJson billingsJson)
    {
        billingsJson.TotalPages = (int)Math.Ceiling((double)billingsJson.Total / billingsJson.PageSize);
        billingsJson.HasNextPage = billingsJson.Page < billingsJson.TotalPages;
        billingsJson.HasPreviousPage = billingsJson.Page > 1;
        return billingsJson;
    }
}

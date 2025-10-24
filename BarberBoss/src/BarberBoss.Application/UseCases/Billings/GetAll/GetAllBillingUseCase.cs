using BarberBoss.Application.Mappers;
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
    public async Task<ResponseBillingsJson> Execute()
    {
        var billings = await _billingReadOnlyRepository.GetAll();
        var result = billings.ToResponseBillingsJson();
        return result;
    }
}

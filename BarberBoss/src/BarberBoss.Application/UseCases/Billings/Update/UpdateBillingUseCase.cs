using BarberBoss.Application.Common.Validation;
using BarberBoss.Application.Mappers;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Billings.Update;
public class UpdateBillingUseCase : IUpdateBillingUseCase
{
    private readonly IBillingUpdateOnlyRepository _updateOnlyBillingRepository;
    private readonly IBillingReadOnlyRepository _readOnlyBillingRepository;

    public UpdateBillingUseCase(IBillingUpdateOnlyRepository updateOnlyBillingRepository, IBillingReadOnlyRepository readOnlyBillingRepository)
    {
        _updateOnlyBillingRepository = updateOnlyBillingRepository;
        _readOnlyBillingRepository = readOnlyBillingRepository;
    }
    public async Task Execute(Guid id, RequestBillingJson request)
    {
        ValidateRequest.Validate(request);
        var billingUpdated = request.ToBillingUpdate();
        var billing = await _readOnlyBillingRepository.GetById(id);
        if (billing is null)
            throw new NotFoundException("Billing not found");
        await _updateOnlyBillingRepository.Update(id, billingUpdated);
    }
}

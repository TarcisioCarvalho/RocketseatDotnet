using BarberBoss.Domain.Repositories;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Billings.Delete;
public class DeleteBillingUseCase : IDeleteBillingUseCase
{
    private readonly IBillingWriteOnlyRepository _billingRepository;
    private readonly IBillingReadOnlyRepository _billingReadOnlyRepository;

    public DeleteBillingUseCase(IBillingWriteOnlyRepository billingRepository, IBillingReadOnlyRepository billingReadOnlyRepository)
    {
        _billingRepository = billingRepository;
        _billingReadOnlyRepository = billingReadOnlyRepository;
    }

    public async Task Execute(Guid id)
    {
        var existingBilling = await _billingReadOnlyRepository.GetById(id);
        if (existingBilling is null)
            throw new NotFoundException($"Billing with ID {id} not found.");
        
        await _billingRepository.Delete(id);
    }
}

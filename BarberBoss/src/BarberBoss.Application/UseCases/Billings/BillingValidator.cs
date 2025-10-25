using BarberBoss.Application.Common.Validation;
using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.UseCases.Billings;
public class BillingValidator
{
    public ValidationResult Validate(RequestBillingJson request)
    {
        var validationResult = new ValidationResult();
        validationResult
            .IsNotEmpty(request.BarberName, "Nome do barbeiro")
            .Length(request.BarberName, "Nome do barbeiro", 2, 80)
            .IsNotEmpty(request.ClientName, "Nome do cliente")
            .Length(request.ClientName, "Nome do cliente", 2, 80)
            .IsNotEmpty(request.ServiceName, "Nome do serviço")
            .Length(request.ServiceName, "Nome do serviço", 2, 80)
            .IsGreaterThan(request.Amount, "Preço do serviço", 0)
            .ValidEnum(request.PaymentMethod, "Método de pagamento")
            .ValidEnum(request.Status, "Status do pagamento")
            .When(request.Status == Status.Canceled,
            r => r.Must(request.Amount == 0, "O valor deve ser 0 para cobranças canceladas"));
        // Add validation logic here
        return validationResult;
    }
}

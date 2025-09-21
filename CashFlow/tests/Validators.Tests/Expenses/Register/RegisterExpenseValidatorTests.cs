using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace Validators.Tests.Expenses.Register;
public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterExpenseValidator();
        var request = new RequestRegisterExpenseJson
        {
            Title = "Valid Title",
            Description = "Valid Description",
            Date = DateTime.Now.AddDays(-1),
            Value = 100.00m,
            PaymentType = PaymentType.creditCard
        };
        var result = validator.Validate(request);
        Assert.True(result.IsValid);
    }
}

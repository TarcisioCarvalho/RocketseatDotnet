using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        // Implementation of the use case to register an expense
        return new ResponseRegisterExpenseJson
        {
            Title = "Sample Expense"
        };
    }
}

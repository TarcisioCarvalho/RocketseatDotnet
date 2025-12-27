using CashFlow.Application.Validation;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using System.Net.Mail;

namespace CashFlow.Application.UseCases.Users.Update;
public class UpdateUserValidator 
{
    public ValidationResult Validate(RequestUpdateUserJson request)
    {
        var result = new ValidationResult();

        if(string.IsNullOrWhiteSpace(request.Name))
        {
            result.AddError("Name", ResourceErrorsMessages.NAME_REQUIRED);
        }
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            result.AddError("Email", "Email is required.");
            return result;
        }
        if (!MailAddress.TryCreate(request.Email, out _))
        {
            result.AddError("Email", "A valid email is required.");
        }

        return result;
    }
}

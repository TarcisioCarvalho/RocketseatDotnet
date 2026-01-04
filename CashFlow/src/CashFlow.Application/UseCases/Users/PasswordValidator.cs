using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.UseCases.Users;
public class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.AddFailure("Password is required.");
            return true;
        }

        if (password.Length < 8)
        {
            context.AddFailure("Password must be at least 8 characters long.");
            return true;
        }

        if (!password.Any(char.IsUpper))
        {
            context.AddFailure("Password must contain at least one uppercase letter.");
            return true;
        }

        if (!password.Any(char.IsLower))
        {
            context.AddFailure("Password must contain at least one lowercase letter.");
            return true;
        }

        if (!password.Any(char.IsDigit))
        {
            context.AddFailure("Password must contain at least one digit.");
            return true;
        }

        if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            context.AddFailure("Password must contain at least one special character.");
            return true;
        }

        return true;
    }
}

namespace CashFlow.Application.Validation;
public class ValidationResult
{
    public bool IsValid { get; private set; }
    public List<ValidationError> Errors { get; private set; }

    public ValidationResult()
    {
        IsValid = true;
        Errors = new List<ValidationError>();
    }

    public void AddError(string propertyName, string errorMessage)
    {
        IsValid = false;
        Errors.Add(new ValidationError
        {
            PropertyName = propertyName,
            ErrorMessage = errorMessage
        });
    }
}

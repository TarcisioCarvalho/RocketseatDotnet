namespace BarberBoss.Application.Common.Validation;
public static class ValidationExtensions
{
    public static ValidationResult IsNotEmpty(this ValidationResult result, string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result.AddError($"{fieldName} não pode ser vazio.");
        }
        return result;
    }
    public static ValidationResult Length(this ValidationResult result, string value, string fieldName,long minLength, long maxLength)
    {
        if (value.Length < minLength || value.Length > maxLength)
        {
            result.AddError($"{fieldName} deve ter entre {minLength} e {maxLength} caracteres.");
        }
        return result;
    }

    public static ValidationResult IsGreaterThan(this ValidationResult result, decimal value, string fieldName, decimal minValue)
    {
        if (value <= minValue)
        {
            result.AddError($"{fieldName} deve ser maior que {minValue}.");
        }
        return result;
    }

    public static ValidationResult ValidEnum<TEnum>(this ValidationResult result,TEnum value, string fieldName) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
        {
            var validValues = string.Join(", ", Enum.GetNames(typeof(TEnum)));
            result.AddError($"{fieldName} é inválido. Valores válidos: {validValues}.");
        }
        return result;
    }

    public static ValidationResult When(this ValidationResult result, bool condition, Func<ValidationResult,ValidationResult> validation)
    {
        if (condition)
        {
           return validation(result);
        }
        return result;
    }

    public static ValidationResult Must(this ValidationResult result, bool condition, string errorMessage)
    {
        if (!condition)
        {
            result.AddError(errorMessage);
        }
        return result;
    }
}

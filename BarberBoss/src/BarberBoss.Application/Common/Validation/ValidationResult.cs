namespace BarberBoss.Application.Common.Validation;
public class ValidationResult
{
    private readonly List<string> _errors = [];
    public IReadOnlyList<string> Errors => _errors;
    public bool IsValid => !_errors.Any();

    public void AddError(string error)
    {
        _errors.Add(error);
    }
    public void AddErrors(IEnumerable<string> errors)
    {
        _errors.AddRange(errors);
    }
    public void Clear()
    {
        _errors.Clear();
    }
}

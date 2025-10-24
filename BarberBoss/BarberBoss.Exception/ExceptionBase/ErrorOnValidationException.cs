using System.Net;

namespace BarberBoss.Exception.ExceptionBase;
public class ErrorOnValidationException : BarberBossException
{
    private readonly IReadOnlyList<string> _errors;
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    public ErrorOnValidationException(IReadOnlyList<string> errors) : base(string.Empty)
    {
        _errors = errors;
    }

    public override IReadOnlyList<string> GetErrors()
    {
        return _errors;
    }
}

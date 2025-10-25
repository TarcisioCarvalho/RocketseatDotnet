
using System.Net;

namespace BarberBoss.Exception.ExceptionBase;
public class NotFoundException : BarberBossException
{
    public NotFoundException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override IReadOnlyList<string> GetErrors()
    {
        return new List<string> { Message };
    }
}

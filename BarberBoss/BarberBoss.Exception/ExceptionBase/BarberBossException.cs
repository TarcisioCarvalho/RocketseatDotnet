
namespace BarberBoss.Exception.ExceptionBase;
public abstract class BarberBossException : SystemException
{
    public BarberBossException(string message) : base(message) { }

    public abstract int StatusCode { get; }
    public abstract IReadOnlyList<string> GetErrors();

}

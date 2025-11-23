using CashFlow.Exception.ExceptionBase;
using System.Net;

namespace CashFlow.Exception.ExceptionsBase
{
    public class InvalidLoginException : CashFlowException
    {
        public InvalidLoginException()
            : base(ResourceErrorsMessages.INVALID_LOGIN)
        {
        }

        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public override List<string> GetErrors()
        {
            return new List<string> { Message };
        }
    }
}
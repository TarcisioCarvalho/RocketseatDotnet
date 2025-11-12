using CashFlow.Domain.Security.Tokens;

namespace CashFlow.API.Token;

public class HttpContextTokenValue : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HttpContextTokenValue(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string TokenOnRequest()
    {
        return _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString().Replace("Bearer ", "").Trim()!;
    }
}

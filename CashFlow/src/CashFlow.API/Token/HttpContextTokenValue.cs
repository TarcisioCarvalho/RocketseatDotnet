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
        var token = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();
        var tokenWithoutBearer = token?.Replace("Bearer ", "").Trim();
        return tokenWithoutBearer!;
    }
}

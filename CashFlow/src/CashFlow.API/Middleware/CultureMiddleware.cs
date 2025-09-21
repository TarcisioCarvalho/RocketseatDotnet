using System.Globalization;

namespace CashFlow.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        var suportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
            .ToList();

        var cultureQuery = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var culture = new CultureInfo("pt-BR");

        if (!string.IsNullOrWhiteSpace(cultureQuery)
            && suportedCultures.Exists(c => c.Name.Equals(cultureQuery)))
        {
                culture = new CultureInfo(cultureQuery);
        }
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        await _next(context);
    }
}

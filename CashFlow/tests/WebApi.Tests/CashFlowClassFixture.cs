using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Tests;
public class CashFlowClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    public CashFlowClassFixture(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    protected async Task<HttpResponseMessage> DoPost(string requestUri, object request, string token = "", string culture = "pt-Br")
    {
        AutorizeRequest(token);
        ChangeRequestCulture(culture);
        return await _httpClient.PostAsJsonAsync(requestUri, request);
    }
    private void AutorizeRequest(string token)
    {
        if (string.IsNullOrEmpty(token))
            return;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private void ChangeRequestCulture(string culture)
    {
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture));
    }

}

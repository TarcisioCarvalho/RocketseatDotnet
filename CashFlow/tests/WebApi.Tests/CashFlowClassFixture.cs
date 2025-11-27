namespace WebApi.Tests;
public class CashFlowClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    public CashFlowClassFixture(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }
}

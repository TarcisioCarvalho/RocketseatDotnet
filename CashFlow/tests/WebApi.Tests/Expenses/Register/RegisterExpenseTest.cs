using CommonTestUtilities.Requests;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Tests.Expenses.Register;
public class RegisterExpenseTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private const string METHOD_URL = "api/Expense";
    public RegisterExpenseTest(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        var response = await _httpClient.PostAsJsonAsync(METHOD_URL, request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        Assert.Equal(request.Title, result.RootElement.GetProperty("title").GetString());
    }

    public async Task Error_Title_Empty()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;
        var response = await _httpClient.PostAsJsonAsync(METHOD_URL, request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}

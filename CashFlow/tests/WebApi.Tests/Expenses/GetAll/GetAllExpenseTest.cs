using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Expenses.GetAll;
public class GetAllExpenseTest : CashFlowClassFixture
{
    private const string METHOD_URL = "api/Expense";
    private string _token = string.Empty;
    public GetAllExpenseTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: METHOD_URL, token: _token);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        var expenses = response.RootElement.GetProperty("expenses").EnumerateArray();
        Assert.True(expenses.Any());
    }

}

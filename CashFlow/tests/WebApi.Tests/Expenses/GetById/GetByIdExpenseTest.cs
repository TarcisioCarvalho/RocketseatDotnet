using CashFlow.Communication.Enums;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Expenses.GetById;
public class GetByIdExpenseTest : CashFlowClassFixture
{
    private const string METHOD_URL = "api/Expense";
    private string _token = string.Empty;
    private long _expenseId = 0;
    public GetByIdExpenseTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.UserTeamMember.GetToken();
        _expenseId = factory.Expense.GetExpenseId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: $"{METHOD_URL}/{_expenseId}", token: _token);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        Assert.Equal(response.RootElement.GetProperty("id").GetInt64(), _expenseId);
        var date = response.RootElement.GetProperty("date").GetDateTime();
        Assert.True(date <= DateTime.Now);
        var paymentType = response.RootElement.GetProperty("paymentType").GetInt32();
        Assert.True(Enum.IsDefined(typeof(PaymentType), paymentType));
    }


}

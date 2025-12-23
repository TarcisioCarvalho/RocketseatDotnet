using CashFlow.Exception;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Expenses.Delete;
public class DeleteExpenseTest : CashFlowClassFixture
{
    private const string METHOD_URL = "api/Expense";
    private string _token = string.Empty;
    private long _expenseId = 0;
    public DeleteExpenseTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.UserTeamMember.GetToken();
        _expenseId = factory.ExpenseTeamMember.GetExpenseId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(requestUri: $"{METHOD_URL}/{_expenseId}", token: _token);
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var result = await DoDelete(requestUri: $"{METHOD_URL}/{10}", token: _token);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        Assert.Single(errors);
        Assert.Equal(ResourceErrorsMessages.EXPENSE_NOT_FOUND, errors.FirstOrDefault().GetString());

    }
}

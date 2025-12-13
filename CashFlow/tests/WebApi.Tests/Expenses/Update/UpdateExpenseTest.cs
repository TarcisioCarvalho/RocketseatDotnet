using CashFlow.Exception;
using CommonTestUtilities.Requests;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Expenses.Update;
public class UpdateExpenseTest : CashFlowClassFixture
{
    private const string METHOD_URL = "api/Expense";
    private string _token = string.Empty;
    private long _expenseId = 0;
    public UpdateExpenseTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.UserTeamMember.GetToken();
        _expenseId = factory.Expense.GetExpenseId();
    }
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();

        var response = await DoPut($"{METHOD_URL}/{_expenseId}", request, _token);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Title_Empty(string cultureInfo)
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;
        var response = await DoPut($"{METHOD_URL}/{_expenseId}", request, _token, cultureInfo);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);

        var errors = result.RootElement.GetProperty("errorMessages").EnumerateArray();
        Assert.Single(errors);
        var expectedMessage = ResourceErrorsMessages.ResourceManager.GetString("TITLE_REQUIRED", new CultureInfo(cultureInfo));
        Assert.Contains(errors, error => error.GetString()!.Equals(expectedMessage));
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Expense_Not_Found(string cultureInfo)
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        var response = await DoPut($"{METHOD_URL}/{_expenseId + 1}", request, _token, cultureInfo);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);

        var errors = result.RootElement.GetProperty("errorMessages").EnumerateArray();
        Assert.Single(errors);
        var expectedMessage = ResourceErrorsMessages.ResourceManager.GetString("EXPENSE_NOT_FOUND", new CultureInfo(cultureInfo));
        Assert.Contains(errors, error => error.GetString()!.Equals(expectedMessage));
    }
}

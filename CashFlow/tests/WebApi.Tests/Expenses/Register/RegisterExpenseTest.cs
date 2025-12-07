using CashFlow.Exception;
using CommonTestUtilities.Requests;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Expenses.Register;
public class RegisterExpenseTest : CashFlowClassFixture
{
    private const string METHOD_URL = "api/Expense";
    private string _token = string.Empty;
    public RegisterExpenseTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.UserTeamMember.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        
        var response = await DoPost(METHOD_URL, request, _token);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        Assert.Equal(request.Title, result.RootElement.GetProperty("title").GetString());
    }
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Title_Empty(string cultureInfo)
    {
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = string.Empty;

    
        var response = await DoPost(METHOD_URL, request, _token, culture: cultureInfo);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);

        var errors = result.RootElement.GetProperty("errorMessages").EnumerateArray();
        Assert.Single(errors);
        var expectedMessage = ResourceErrorsMessages.ResourceManager.GetString("TITLE_REQUIRED", new CultureInfo(cultureInfo));
        Assert.Contains(errors, error => error.GetString()!.Equals(expectedMessage));
    }
}

using System.Net;
using System.Net.Mime;

namespace WebApi.Tests.Expenses.Reports;
public class GenerateExpensesReportsTest : CashFlowClassFixture
{
    private const string METHOD_URL = "api/Report";
    private readonly string _adminToken;
    private readonly string _teamMemberToken;
    private readonly DateTime _expenseDate;

    public GenerateExpensesReportsTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _adminToken = factory.UserAdminMember.GetToken();
        _teamMemberToken = factory.UserTeamMember.GetToken();
        _expenseDate = factory.ExpenseAdminMember.GetDate();
    }

    [Fact]
    public async Task Success_Pdf()
    {
        var result = await DoGet(requestUri: $"{METHOD_URL}/pdf?month={_expenseDate:Y}", _adminToken);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result.Content.Headers.ContentType);
        Assert.Equal(MediaTypeNames.Application.Pdf, result.Content.Headers.ContentType!.MediaType);
    }
    [Fact]
    public async Task Success_Excel()
    {
        var result = await DoGet(requestUri: $"{METHOD_URL}/excel?month={_expenseDate:Y}", _adminToken);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result.Content.Headers.ContentType);
        Assert.Equal(MediaTypeNames.Application.Octet, result.Content.Headers.ContentType!.MediaType);
    }

    [Fact]
    public async Task Error_Forbiden_User_Not_Allowed_Excel()
    {
        var result = await DoGet(requestUri: $"{METHOD_URL}/excel?month={_expenseDate:Y}", _teamMemberToken);
        Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
    }

    [Fact]
    public async Task Error_Forbiden_User_Not_Allowed_Pdf()
    {
        var result = await DoGet(requestUri: $"{METHOD_URL}/pdf?month={_expenseDate:Y}", _teamMemberToken);
        Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
    }
}

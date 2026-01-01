using CommonTestUtilities.Requests;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Users.Update;
public class UpdateUserTest : CashFlowClassFixture
{
    private const string requestUri = "api/User";
    private string _token = string.Empty;
    public UpdateUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.UserTeamMember.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestUpdateUserJsonBuilder.Build();
        var response = await DoPut(requestUri, request, _token);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}

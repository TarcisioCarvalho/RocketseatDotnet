using System.Net;

namespace WebApi.Tests.Users.Delete;
public class DeleteUserTest : CashFlowClassFixture
{
    private readonly string _token = string.Empty;
    private const string requestUri = "api/User";
    public DeleteUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.UserTeamMember.GetToken();
    }

    [Fact]
    public async Task Success()
    {
       var response = await DoDelete(requestUri: requestUri, token: _token);
       Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}

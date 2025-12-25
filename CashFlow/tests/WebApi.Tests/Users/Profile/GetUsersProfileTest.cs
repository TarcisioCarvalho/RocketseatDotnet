using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Users.Profile;
public class GetUsersProfileTest : CashFlowClassFixture
{
    private const string requestUri = "api/User";
    private string _token = string.Empty;
    private string _name  = string.Empty;
    private string _email = string.Empty;
  
    public GetUsersProfileTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.UserTeamMember.GetToken();
        _name = factory.UserTeamMember.GetName();
        _email = factory.UserTeamMember.GetEmail();
    }

    [Fact]
    public async Task Success()
    {
        var response = await DoGet(requestUri, _token);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);

        var resultName = result.RootElement.GetProperty("name").GetString();
        var resultEmail = result.RootElement.GetProperty("email").GetString();

        Assert.Equal(_name, resultName);
        Assert.Equal(_email, resultEmail);
    }
}

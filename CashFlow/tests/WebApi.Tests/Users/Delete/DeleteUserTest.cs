using CashFlow.Communication.Requests;
using System.Net;
using System.Text.Json;

namespace WebApi.Tests.Users.Delete;
public class DeleteUserTest : CashFlowClassFixture
{
    private readonly string _token = string.Empty;
    private string _email = string.Empty;
    private string _name = string.Empty;
    private string _password = string.Empty;
    private const string requestUri = "api/User";
    private const string requestUriLogin = "api/Login";
    public DeleteUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.UserTeamMember.GetToken();
        _email = factory.UserTeamMember.GetEmail();
        _name = factory.UserTeamMember.GetName();
        _password = factory.UserTeamMember.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password
        };
        var responseLogin = await DoPost(requestUri: requestUriLogin, request);
        Assert.Equal(HttpStatusCode.OK, responseLogin.StatusCode);

        var body = await responseLogin.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);

        var token = result.RootElement.GetProperty("token").GetString();

        var response = await DoDelete(requestUri: requestUri, token: token);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var responseLoginAfterDelete = await DoPost(requestUri: requestUriLogin, request);
        Assert.Equal(HttpStatusCode.Unauthorized, responseLoginAfterDelete.StatusCode);
    }
}

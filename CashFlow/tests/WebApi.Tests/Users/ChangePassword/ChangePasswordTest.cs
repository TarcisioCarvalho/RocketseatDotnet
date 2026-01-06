using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Users.ChangePassword;
public class ChangePasswordTest : CashFlowClassFixture
{
    private const string METHOD = "api/User/change-password";
    private readonly string _password;
    private readonly string _email;
    private readonly string _token;
    public ChangePasswordTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _email = factory.UserTeamMember.GetEmail();
        _token = factory.UserTeamMember.GetToken();
        _password = factory.UserTeamMember.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestJsonChangePasswordBuilder.Build();
        request.Password = _password;

        var response = await DoPut(METHOD, request, token: _token);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var loginRequest = new RequestLoginJson() { Email = _email, Password = _password };
        response = await DoPost(requestUri: "api/Login", request: loginRequest);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

        loginRequest.Password = request.NewPassword;
        response = await DoPost(requestUri: "api/Login", request: loginRequest);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Password_Is_Diferent_Current_Password(string culture)
    {
        var request = RequestJsonChangePasswordBuilder.Build();
        var response = await DoPut(METHOD, request, token: _token, culture: culture);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);

        var errors = result.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage = ResourceErrorsMessages.ResourceManager.GetString("PASSWORD_DIFERENT_CURRENT_PASSWORD", new CultureInfo(culture));

        Assert.Single(errors);
        Assert.Equal(expectedMessage, errors.First().ToString());

    }
}

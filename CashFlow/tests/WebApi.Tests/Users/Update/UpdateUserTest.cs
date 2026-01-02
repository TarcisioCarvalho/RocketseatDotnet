using CashFlow.Exception;
using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Authentication;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

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

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name(string culture)
    {
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Name = string.Empty;
        var response = await DoPut(requestUri, request, _token, culture);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);

        var errors = result.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage = ResourceErrorsMessages.ResourceManager.GetString("NAME_REQUIRED", new CultureInfo(culture));

        Assert.Single(errors);
        Assert.Equal(errors.First().GetString(), expectedMessage);
    }
}

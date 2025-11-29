using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Globalization;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Users.Register;
public class RegisterUserTest : CashFlowClassFixture
{

    private const string requestUri = "api/User";

    public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory){}

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var response = await DoPost(requestUri:requestUri,request);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);

        result.RootElement.GetProperty("name").GetString().Should().Be(request.Name);
        result.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Empty(string culture)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var response = await DoPost(requestUri:requestUri,request,culture:culture);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);

        var errors = result.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage = ResourceErrorsMessages.ResourceManager.GetString("NAME_REQUIRED", new CultureInfo(culture));
        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}

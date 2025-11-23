using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.Login.DoLogin;
public class DoLoginTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private const string METHOD_URL = "api/Login";
    private string _email = string.Empty;
    private string _name = string.Empty;
    private string _password = string.Empty;

    public DoLoginTest(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
        _email = factory.GetEmail();
        _name = factory.GetName();
        _password = factory.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password
        };
        var response = await _httpClient.PostAsJsonAsync(METHOD_URL, request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        result.RootElement.GetProperty("name").GetString().Should().Be(_name);
        result.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Invalid_Login(string cultureInfo)
    {
        var request = RequestJsonLoginBuilder.Build(null);
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(cultureInfo));
        var response = await _httpClient.PostAsJsonAsync(METHOD_URL, request);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        var errors = result.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedMessage = ResourceErrorsMessages.ResourceManager.GetString("INVALID_LOGIN", new CultureInfo(cultureInfo));
       errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}

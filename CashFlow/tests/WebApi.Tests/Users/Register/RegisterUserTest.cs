using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Tests.Users.Register;
public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private const string METHOD_URL = "api/User";

    public RegisterUserTest(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var response = await _httpClient.PostAsJsonAsync(METHOD_URL, request);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        Assert.Equal(request.Name, result.RootElement.GetProperty("name").GetString());
        Assert.NotNull(result.RootElement.GetProperty("token").GetString());
        Assert.NotEmpty(result.RootElement.GetProperty("token").GetString());
        //response.EnsureSuccessStatusCode();
    }
}

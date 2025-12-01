using CashFlow.Application.UseCases.Users.Login;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Security;
using FluentAssertions;

namespace UseCases.Tests.Login.DoLogin;
public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var loginRequest = RequestJsonLoginBuilder.Build(user.Email);
        var loginUseCase = CreateUseCase(user, loginRequest.Password);
        var response = await loginUseCase.Execute(loginRequest);
        Assert.NotNull(response);
        Assert.NotNull(response.Token);
        Assert.NotEmpty(response.Token);
        Assert.Equal(user.Name, response.Name);
        response.Name.Should().Be(user.Name);
        response.Should().NotBeNull();
        response.Token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_User_Not_Found()
    {
        var user = UserBuilder.Build();
        var loginRequest = RequestJsonLoginBuilder.Build(null);
        var loginUseCase = CreateUseCase(user, loginRequest.Password);
        Func<Task> action = async () => await loginUseCase.Execute(loginRequest);
        var exception = await Assert.ThrowsAsync<InvalidLoginException>(async () => await action());
        Assert.Single(exception.GetErrors());
        Assert.Equal(ResourceErrorsMessages.INVALID_LOGIN, exception.GetErrors().First());
    }

    [Fact]
    public async Task Error_Password_Not_Match()
    {
        var user = UserBuilder.Build();
        var loginRequest = RequestJsonLoginBuilder.Build(user.Email);
        var loginUseCase = CreateUseCase(user);
        Func<Task> action = async () => await loginUseCase.Execute(loginRequest);
        var exception = await Assert.ThrowsAsync<InvalidLoginException>(async () => await action());
        Assert.Single(exception.GetErrors());
        Assert.Equal(ResourceErrorsMessages.INVALID_LOGIN, exception.GetErrors().First());
    }

    private DoLoginUseCase CreateUseCase(User user,string? password = null)
    {
        var passwordEncripter = new PasswordEncripterBuilder().Verify(password).Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readUserRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();

        return new DoLoginUseCase(
            readUserRepository,
            passwordEncripter,
            tokenGenerator
        );
    }
}

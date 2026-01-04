using CashFlow.Application.UseCases.Users.ChangePassword;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Security;

namespace UseCases.Tests.Users.ChangePassword;
public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var request = RequestJsonChangePasswordBuilder.Build();
        var useCase = CreateUseCase(user, request.Password);
        await useCase.Execute(request);
    }

    [Fact]
    public async Task Error_New_Password_Empty()
    {
        var user = UserBuilder.Build();
        var request = RequestJsonChangePasswordBuilder.Build();
        request.NewPassword = string.Empty;
        var useCase = CreateUseCase(user, request.Password);
        var act = async () => await useCase.Execute(request);
        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(() => act());
        Assert.Single(exception.GetErrors());
        Assert.Equal("Password is required.", exception.GetErrors().First());

        // Password is required.
    }
    private IChangePasswordUseCase CreateUseCase(User user, string? password = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user.Id, user).Build();
        var passwordEncripter = new PasswordEncripterBuilder().Verify(password).Build();

        return new ChangePasswordUseCase(loggedUser, passwordEncripter, userUpdateRepository, unitOfWork);
    }
}

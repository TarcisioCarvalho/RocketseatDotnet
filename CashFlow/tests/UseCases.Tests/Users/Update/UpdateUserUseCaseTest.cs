using CashFlow.Application.UseCases.Users.Update;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;

namespace UseCases.Tests.Users.Update;
public class UpdateUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = RequestUpdateUserJsonBuilder.Build();
        await useCase.Execute(request);
        Assert.Equal(user.Name, request.Name);
        Assert.Equal(user.Email, request.Email);
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Name = string.Empty;

        Func<Task> action = async () => await useCase.Execute(request);
        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(() => action());

        Assert.Single(exception.GetErrors());
        Assert.Equal(ResourceErrorsMessages.NAME_REQUIRED, exception.GetErrors().First());
    }

    [Fact]
    public async Task Error_Email_Already_Exist()
    {
        var user = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        var useCase = CreateUseCase(user, request.Email);

        Func<Task> action = async () => await useCase.Execute(request);
        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(() => action());

        Assert.Single(exception.GetErrors());
        Assert.Equal("Já existe um usuário cadastrado com este e-mail.", exception.GetErrors().First());
    }


    private IUpdateUserUseCase CreateUseCase(User user, string? email = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();

        var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        var userUpdateOnlyRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user.Id, user).Build();
        var userReadOnlyRepository = userReadOnlyRepositoryBuilder.Build();
        
        if (email != null)
        {
            userReadOnlyRepository = userReadOnlyRepositoryBuilder.ExistActiveUserWith(email).Build();
        }

        return new UpdateUserUseCase(userReadOnlyRepository, loggedUser, userUpdateOnlyRepository, unitOfWork);
    }
}

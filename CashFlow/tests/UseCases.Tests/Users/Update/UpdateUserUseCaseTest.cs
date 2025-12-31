using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.Users.Update;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
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


    private IUpdateUserUseCase CreateUseCase(User user)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();
        var userUpdateOnlyRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user.Id, user).Build();
        return new UpdateUserUseCase(userReadOnlyRepository, loggedUser, userUpdateOnlyRepository, unitOfWork);
    }
}

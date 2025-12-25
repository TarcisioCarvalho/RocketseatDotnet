using CashFlow.Application.UseCases.Users.Profile;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;

namespace UseCases.Tests.Users.Profile;
public class GetUserProfileUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var result = await useCase.Execute();
        Assert.NotNull(result);
        Assert.Equal(user.Name, result.Name);
        Assert.Equal(user.Email, result.Email);
    }



    private GetUserProfileUseCase CreateUseCase(User user)
    {
        var mapper =  MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        return new GetUserProfileUseCase(loggedUser, mapper);
    }
}

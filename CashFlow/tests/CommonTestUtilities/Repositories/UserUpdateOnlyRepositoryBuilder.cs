using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IUserUpdateOnlyRepository> _userUpdateOnlyRepositoryMock;

    public UserUpdateOnlyRepositoryBuilder()
    {
        _userUpdateOnlyRepositoryMock = new Mock<IUserUpdateOnlyRepository>();
    }

    public UserUpdateOnlyRepositoryBuilder GetById(long id, User user)
    {
        _userUpdateOnlyRepositoryMock.Setup(repo => repo.GetById(id)).ReturnsAsync(user);
        return this;
    }

    public IUserUpdateOnlyRepository Build() => _userUpdateOnlyRepositoryMock.Object;
}

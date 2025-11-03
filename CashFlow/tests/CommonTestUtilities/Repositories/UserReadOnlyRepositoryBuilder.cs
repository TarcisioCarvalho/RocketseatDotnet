using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepository;
    public UserReadOnlyRepositoryBuilder()
    {
        _userReadOnlyRepository = new Mock<IUserReadOnlyRepository>();
    }

    public void ExistActiveUserWith(string email)
    {
        _userReadOnlyRepository.Setup(repo => repo.ExistActiveUserWith(email)).ReturnsAsync(true);
    }

    public UserReadOnlyRepositoryBuilder GetUserByEmail(User user)
    {
         _userReadOnlyRepository.Setup(repo => repo.GetUserByEmail(user.Email)).ReturnsAsync(user);
         return this;
    }
    public IUserReadOnlyRepository Build() => _userReadOnlyRepository.Object;
}

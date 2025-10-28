namespace CashFlow.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWith(string email);
}



namespace CashFlow.Domain.Repositories.User;
public interface IUserUpdateOnlyRepository
{
    Task<CashFlow.Domain.Entities.User?> GetById(long id);
    void Update(CashFlow.Domain.Entities.User user);
}

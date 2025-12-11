using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;

namespace UseCases.Tests.Expenses.Update;
public class UpdateExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUpdateUseCase(user);
       // var result = useCase.Execute();
    }
    private IUpdateExpenseUseCase CreateUpdateUseCase(User user)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var mapper = MapperBuilder.Build();
        var repository = new ExpensesUpdateOnlyRepositoryBuilder().Build();

        return new UpdateExpenseUseCase(expenseRepository: repository, unitOfWork: unitOfWork, mapper: mapper, loggedUser:loggedUser);
    }
}

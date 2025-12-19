using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Tests.Resources;

namespace WebApi.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{

    public ExpenseIdentityManager ExpenseTeamMember { get; private set; } = default!;
    public ExpenseIdentityManager ExpenseAdminMember { get; private set; } = default!;
    public UserIdentityManager UserTeamMember { get; private set; } = default!;
    public UserIdentityManager UserAdminMember { get; private set; } = default!;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
                services.AddDbContext<CashFlowDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });
                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
                var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();
                var tokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();
                StartDatabase(dbContext, passwordEncripter, tokenGenerator);

            });

    }




    private void StartDatabase(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator tokenGenerator)
    {
        var user = AddUserTeamMeber(dbContext, passwordEncripter, tokenGenerator);
        var expenseTeamMember  = AddExpenses(dbContext, user, id: 1);
        ExpenseTeamMember = new ExpenseIdentityManager(expenseTeamMember);
        var userAdmin = AddUserAdminMember(dbContext, passwordEncripter, tokenGenerator);
        var expenseAdminMember  = AddExpenses(dbContext, userAdmin, id: 2);
        ExpenseAdminMember = new ExpenseIdentityManager(expenseAdminMember);

        dbContext.SaveChanges();
    }


    private User AddUserTeamMeber(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator tokenGenerator)
    {
        var user = UserBuilder.Build();
        user.Id = 1;
        var password = user.Password;
        user.Password = passwordEncripter.Encript(password);
        dbContext.Users.Add(user);
        var token = tokenGenerator.Generate(user);

        UserTeamMember = new UserIdentityManager(user, password, token);

        return user;
    }

    private User AddUserAdminMember(CashFlowDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator tokenGenerator)
    {
        var user = UserBuilder.Build(role: Roles.ADMIN);
        user.Id = 2;
        var password = user.Password;
        user.Password = passwordEncripter.Encript(password);
        dbContext.Users.Add(user);
        var token = tokenGenerator.Generate(user);

        UserAdminMember = new UserIdentityManager(user, password, token);

        return user;
    }
    private Expense AddExpenses(CashFlowDbContext dbContext, User user, long id)
    {
        var expense = ExpenseBuilder.Build(user, id);
        dbContext.Expenses.Add(expense);

        return expense;
    }
}

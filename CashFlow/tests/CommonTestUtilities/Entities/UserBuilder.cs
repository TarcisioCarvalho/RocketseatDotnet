using Bogus;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Security;

namespace CommonTestUtilities.Entities;
public class UserBuilder
{
    public static User Build()
    {
        var passwordEncripter = new PasswordEncripterBuilder().Build();
        var user = new Faker<User>()
            .RuleFor(u => u.Id, _ => 1)
            .RuleFor(u => u.Name, f => f.Person.FirstName)
            .RuleFor(u => u.Email, (f,user) => f.Internet.Email(user.Name))
            .RuleFor(u => u.Password, f => passwordEncripter.Encript(f.Internet.Password()))
            .RuleFor(u => u.UserIndetifier, _ => Guid.NewGuid());

        return user;
    }
}

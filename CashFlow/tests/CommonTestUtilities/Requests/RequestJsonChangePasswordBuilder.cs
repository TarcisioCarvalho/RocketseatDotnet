
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestJsonChangePasswordBuilder
{
    public static RequestChangePasswordJson Build()
    {
        return new Bogus.Faker<RequestChangePasswordJson>().
          RuleFor(user => user.Password, faker => faker.Internet.Password()).
          RuleFor(user => user.NewPassword, (faker, user) => faker.Internet.Password(length: 8, prefix: "!Aa1"));
    }
}

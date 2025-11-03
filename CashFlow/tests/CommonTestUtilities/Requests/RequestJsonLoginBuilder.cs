using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestJsonLoginBuilder
{
    public static RequestLoginJson Build(string? email)
    {
        return new Bogus.Faker<RequestLoginJson>().
          RuleFor(user => user.Email, faker => email ?? faker.Internet.Email()).
          RuleFor(user => user.Password, faker => faker.Internet.Password(length: 8, prefix: "!Aa1"));
    }
}

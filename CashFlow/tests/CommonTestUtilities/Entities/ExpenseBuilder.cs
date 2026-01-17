using Bogus;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;


namespace CommonTestUtilities.Entities;
public class ExpenseBuilder
{
    public static IList<Expense> Collection(User loggedUser, uint count = 2)
    {
        var expenses = new List<Expense>();

        if (count == 0)
            count = 1;

        for (int i = 1; i <= count; i++)
        {
            var expense = Build(loggedUser, i);
            expenses.Add(expense);
        }

        return expenses;
    }
    public static Expense Build(User user, long? id)
    {
        var expense = new Faker<Expense>()
            .RuleFor(e => e.Id, f => id ?? f.UniqueIndex)
            .RuleFor(e => e.Title, f => f.Commerce.ProductName())
            .RuleFor(e => e.PaymentType, f => f.PickRandom<PaymentType>())
            .RuleFor(e => e.Value, f => f.Finance.Amount())
            .RuleFor(e => e.Description, f => f.Commerce.ProductDescription())
            .RuleFor(e => e.Date, f => f.Date.Past())
            .RuleFor(e => e.UserId, _ => user.Id)
            .RuleFor(e => e.Tags, (f, e) => f.Make(1, () => new CashFlow.Domain.Entities.Tag
            {
                Id = f.UniqueIndex,
                Expense = e,
                ExpenseId = e.Id,
                TagValue = f.PickRandom<CashFlow.Domain.Enums.Tag>()
            }))
            .Generate();

        return expense;
    }
}

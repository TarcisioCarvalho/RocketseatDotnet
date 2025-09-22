﻿using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestRegisterExpenseJsonBuilder
{
    public RequestRegisterExpenseJson Build()
    {
        var faker = new Faker();
        return new Faker<RequestRegisterExpenseJson>()
            .RuleFor(r => r.Title, faker.Commerce.ProductName)
            .RuleFor(r => r.Description, faker.Commerce.ProductDescription)
            .RuleFor(r => r.Date, faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker.PickRandom<PaymentType>())
            .RuleFor(r => r.Value, faker.Random.Decimal(min: 1, max: 100));
    }
}

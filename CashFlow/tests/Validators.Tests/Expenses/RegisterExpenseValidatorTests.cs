using CashFlow.Application.UseCases.Expenses;
using CashFlow.Exception;
using CommonTestUtilities.Requests;

namespace Validators.Tests.Expenses;
public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new ExpenseValidator();
        
        var request = RequestRegisterExpenseJsonBuilder.Build();
        var result = validator.Validate(request);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = title;

        var result = validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ResourceErrorsMessages.TITLE_REQUIRED, result.Errors[0].ErrorMessage);
    }
    [Fact]
    public void Error_Title_TooLong()
    {
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = new string('a', 101);
        var result = validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ResourceErrorsMessages.TITLE_CHARACTERS_LIMIT, result.Errors[0].ErrorMessage);
    }

    [Fact]
    public void Error_Tag_Invalid()
    {
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Tags.Add((CashFlow.Communication.Enums.Tag)100);

        var result = validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ResourceErrorsMessages.TAG_TYPE_NOT_SUPORTED, result.Errors[0].ErrorMessage);
    }
}

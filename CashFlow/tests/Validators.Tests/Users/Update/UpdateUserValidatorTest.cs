using CashFlow.Application.UseCases.Users.Update;
using CommonTestUtilities.Requests;

namespace Validators.Tests.Users.Update;
public class UpdateUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new UpdateUserValidator();
        var request = RequestUpdateUserJsonBuilder.Build();
        var result = validator.Validate(request);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData(null)]
    public void Error_Invalid_Name(string name)
    {
        var validator = new UpdateUserValidator();
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Name = name;
        var result = validator.Validate(request);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("", "Email is required.")]
    [InlineData("      ", "Email is required.")]
    [InlineData(null, "Email is required.")]
    [InlineData("tarcisio", "A valid email is required.")]
    public void Error_Invalid_Email(string email, string teste)
    {
        var validator = new UpdateUserValidator();
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Email = email;
        var result = validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any(erro => erro.ErrorMessage == teste));
    }
}

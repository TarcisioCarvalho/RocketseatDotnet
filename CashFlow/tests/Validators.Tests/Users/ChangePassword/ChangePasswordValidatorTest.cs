using CashFlow.Application.UseCases.Users.ChangePassword;
using CommonTestUtilities.Requests;

namespace Validators.Tests.Users.ChangePassword;
public class ChangePasswordValidatorTest
{
    [Fact]
    public void Success()
    {
        var request = RequestJsonChangePasswordBuilder.Build();
        var result = new ChangePasswordValidator().Validate(request);
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Error_Password_Empty(string password)
    {
        var request = RequestJsonChangePasswordBuilder.Build();
        request.NewPassword = password;
        var result = new ChangePasswordValidator().Validate(request);
        Assert.False(result.IsValid); 
    }
}

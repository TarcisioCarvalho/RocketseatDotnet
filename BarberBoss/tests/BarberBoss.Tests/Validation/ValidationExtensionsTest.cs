using BarberBoss.Application.Common.Validation;
using Xunit;

namespace BarberBoss.Tests.Validation;
public class ValidationExtensionsTest
{
    [Fact]
    public void IsNotEmpty_ComStringVazia_DeveAdicionarErro()
    {
        var validationResult = new ValidationResult();
        validationResult = validationResult.IsNotEmpty("", "nome");
        Assert.False(validationResult.IsValid);
    }
}

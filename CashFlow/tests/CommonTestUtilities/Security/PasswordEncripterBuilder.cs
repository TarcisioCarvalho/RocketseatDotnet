using CashFlow.Domain.Security.Cryptography;
using Moq;

namespace CommonTestUtilities.Security;
public class PasswordEncripterBuilder
{
    private readonly Mock<IPasswordEncripter> _mock;
    public PasswordEncripterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();
        _mock.Setup(passwordEncripter => passwordEncripter.Encript(It.IsAny<string>())).Returns("!@Abcde1");
    }

    public PasswordEncripterBuilder Verify(string? password)
    {
        if(string.IsNullOrEmpty(password))
            return this;

        _mock.Setup(passwordEncripter => passwordEncripter.Verify(password, It.IsAny<string>())).Returns(true);
        return this;
    }
    public IPasswordEncripter Build() => _mock.Object;

}

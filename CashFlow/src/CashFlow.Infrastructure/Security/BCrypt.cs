using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace CashFlow.Infrastructure.Security;
public class BCrypt : IPasswordEncripter
{
    public string Encript(string password) => BC.HashPassword(password);
    public bool Verify(string password, string passwordHash) =>  BC.Verify(password, passwordHash);
}

using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Users.Login;
public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }
    public async Task<ResponseRegisterUserJson> Execute(RequestLoginJson request)
    {
        var user = await _userReadOnlyRepository.GetUserByEmail(request.Email);
        if (user is null)
            throw new InvalidLoginException();
        var matchPassword = _passwordEncripter.Verify(request.Password,user.Password);
        if (!matchPassword)
            throw new InvalidLoginException();

        var token = _accessTokenGenerator.Generate(user);
        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = token
        };
    }
}

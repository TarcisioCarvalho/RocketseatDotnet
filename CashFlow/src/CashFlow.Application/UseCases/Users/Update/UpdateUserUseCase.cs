using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories.User;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Users.Update;
internal class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserReadOnlyRepository _userRepository;

    public UpdateUserUseCase(IUserReadOnlyRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Execute(RequestUpdateUserJson requestUpdateUserJson)
    {
        await Validate(requestUpdateUserJson);
        throw new NotImplementedException();
    }

    private async Task Validate(RequestUpdateUserJson requestUpdateUserJson)
    {
        var result = new UpdateUserValidator().Validate(requestUpdateUserJson);
        var emailAlreadyExists = await _userRepository.ExistActiveUserWith(requestUpdateUserJson.Email);
        if (emailAlreadyExists)
        {
            result.AddError("Email", "Já existe um usuário cadastrado com este e-mail.");
        }
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }

    }
}

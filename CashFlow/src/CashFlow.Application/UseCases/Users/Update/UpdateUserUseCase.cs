using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Users.Update;
internal class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserReadOnlyRepository _userRepository;
    private readonly ILoggedUser _loggedUser;
    private IUserUpdateOnlyRepository _userUpdateOnlyRepository;
    private IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(IUserReadOnlyRepository userRepository, ILoggedUser loggedUser, IUserUpdateOnlyRepository userUpdateOnlyRepository, IUnitOfWork unitOfWork )
    {
        _userRepository = userRepository;
        _loggedUser = loggedUser;
        _userUpdateOnlyRepository = userUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestUpdateUserJson requestUpdateUserJson)
    {
        var loggedUser =  await _loggedUser.Get();
        await Validate(requestUpdateUserJson, loggedUser);
        var user = await _userUpdateOnlyRepository.GetById(loggedUser.Id);
        if (user == null) 
        {
            throw new NotFoundException("Usuário não encontrado");
        }
        user.Name = requestUpdateUserJson.Name;
        user.Email = requestUpdateUserJson.Email;
        _userUpdateOnlyRepository.Update(user);
        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserJson requestUpdateUserJson, User user)
    {
        var result = new UpdateUserValidator().Validate(requestUpdateUserJson);
        var emailAlreadyExists = await _userRepository.ExistActiveUserWith(requestUpdateUserJson.Email);
        if (emailAlreadyExists)
        {
            result.AddError("Email", "Já existe um usuário cadastrado com este e-mail.");
        }
        var isNameAndEmailEqualRequest = requestUpdateUserJson.Name == user.Name && requestUpdateUserJson.Email == user.Email;
        if (isNameAndEmailEqualRequest)
        {
            result.AddError("Cadastro", "Email e nome já cadastrado.");
        }
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }

    }
}

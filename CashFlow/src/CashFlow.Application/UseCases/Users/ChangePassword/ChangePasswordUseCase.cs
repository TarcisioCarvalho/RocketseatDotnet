using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.ChangePassword;
public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePasswordUseCase(ILoggedUser loggedUser, IPasswordEncripter passwordEncripter, IUserUpdateOnlyRepository userUpdateOnlyRepository, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _passwordEncripter = passwordEncripter;
        _userUpdateOnlyRepository = userUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestChangePasswordJson requestChangePasswordJson)
    {
        var loggedUser = await _loggedUser.Get();
        Validate(requestChangePasswordJson, loggedUser);

        var user = await _userUpdateOnlyRepository.GetById(loggedUser.Id);
        if (user is not null)
        {
            user.Password = _passwordEncripter.Encript(requestChangePasswordJson.NewPassword);
            _userUpdateOnlyRepository.Update(user);
            await _unitOfWork.Commit();
        }
    }


    private void Validate(RequestChangePasswordJson requestChangePasswordJson, User loggedUser)
    {
        var valdiator = new ChangePasswordValidator();
        var result = valdiator.Validate(requestChangePasswordJson);
        var passwordMatch = _passwordEncripter.Verify(requestChangePasswordJson.Password, loggedUser.Password);
        if (passwordMatch is false)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorsMessages.PASSWORD_DIFERENT_CURRENT_PASSWORD));
        if (result.IsValid is false)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}

using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Security;
using FluentAssertions;

namespace UseCases.Tests.Users.Register;
public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();
        var response = await useCase.Execute(request);
        response.Should().NotBeNull();
        response.Name.Should().Be(request.Name);
        response.Token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        var useCase = CreateUseCase();
        Func<Task> action = async () => await useCase.Execute(request);
        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(() => action());
        await action.Should().ThrowAsync<ErrorOnValidationException>();

        Assert.Single(exception.GetErrors());
        Assert.Equal("Name is required.", exception.GetErrors().First());
    }

    [Fact]
    public async Task Error_Email_Already_Exists()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);
        Func<Task> action = async () => await useCase.Execute(request);
        var exception = await Assert.ThrowsAsync<ErrorOnValidationException>(() => action());
        await action.Should().ThrowAsync<ErrorOnValidationException>();
        Assert.Single(exception.GetErrors());
        Assert.Equal("Já existe um usuário cadastrado com este e-mail.", exception.GetErrors().First());
    }

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper  = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeUserRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncripter =new PasswordEncripterBuilder().Build();
        var tokenGenerator = JwtTokenGeneratorBuilder.Build();
        var readUserRepository = new UserReadOnlyRepositoryBuilder();
        if(email != null)
        {
            readUserRepository.ExistActiveUserWith(email);
        }
        return new RegisterUserUseCase(mapper,passwordEncripter,readUserRepository.Build(),writeUserRepository,unitOfWork,tokenGenerator);
    }
}

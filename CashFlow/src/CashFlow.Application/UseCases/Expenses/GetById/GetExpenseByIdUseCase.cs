using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
    private readonly IExepensesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    public GetExpenseByIdUseCase(IExepensesReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var expense = await _repository.GetById(id);
        if (expense is null)
            throw new NotFoundException(ResourceErrorsMessages.EXPENSE_NOT_FOUND);
        return _mapper.Map<ResponseExpenseJson>(expense);
    }
}

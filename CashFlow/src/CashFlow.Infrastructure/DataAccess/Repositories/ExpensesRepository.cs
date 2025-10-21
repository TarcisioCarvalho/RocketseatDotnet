using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class ExpensesRepository : IExepensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;
    public ExpensesRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<bool> Delete(long id)
    {
        var expense = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (expense is null) return false;
        _dbContext.Expenses.Remove(expense);
        return true;
    }

    public async Task<IList<Expense>> GetAll()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }
    async Task<Expense?> IExepensesReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

    public async Task<IList<Expense>> FilterByMonth(DateOnly month)
    {
       var startDate = new DateTime(month.Year, month.Month, 1).Date;
       var endDate = startDate.AddMonths(1).AddDays(-1).Date;

        return await _dbContext.
            Expenses.
            AsNoTracking().
            Where(expense => expense.Date >= startDate && expense.Date <= endDate).
            OrderBy(expense => expense.Date).
            ThenBy(expense => expense.Title).
            ToListAsync();
    
    }
}

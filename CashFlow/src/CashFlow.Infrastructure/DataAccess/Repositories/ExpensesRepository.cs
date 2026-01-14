using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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

    public async Task<bool> Delete(long id, User user)
    {
        var expense = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserId == user.Id);
        if (expense is null) return false;
        _dbContext.Expenses.Remove(expense);
        return true;
    }

    public async Task<IList<Expense>> GetAll(User user)
    {
        return await _dbContext.Expenses.AsNoTracking().Where(e => e.UserId == user.Id).ToListAsync();
    }
    async Task<Expense?> IExepensesReadOnlyRepository.GetById(long id, User user)
    {
        return await GetFullExpense().
            AsNoTracking().FirstOrDefaultAsync(e => e.Id == id && e.UserId == user.Id);
    }

    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id, User user)
    {
        return await GetFullExpense().
            FirstOrDefaultAsync(e => e.Id == id && e.UserId == user.Id);
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

    public async Task<IList<Expense>> FilterByMonth(Domain.Entities.User user, DateOnly month)
    {
       var startDate = new DateTime(month.Year, month.Month, 1).Date;
       var endDate = startDate.AddMonths(1).AddDays(-1).Date;

        return await _dbContext.
            Expenses.
            AsNoTracking().
            Where(expense => expense.UserId == user.Id && expense.Date >= startDate && expense.Date <= endDate).
            OrderBy(expense => expense.Date).
            ThenBy(expense => expense.Title).
            ToListAsync();
    
    }

    private IIncludableQueryable<Expense, ICollection<Tag>> GetFullExpense()
    {
        return  _dbContext.Expenses.Include(expense => expense.Tags);
    }
}

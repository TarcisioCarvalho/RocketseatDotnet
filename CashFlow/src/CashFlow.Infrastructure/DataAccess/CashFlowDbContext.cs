using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;
internal class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       optionsBuilder.UseMySql("Server=localhost;Database=cashflowdb;User=root;Password=dono;", new MySqlServerVersion(new Version(8, 0, 40)));

    }
}

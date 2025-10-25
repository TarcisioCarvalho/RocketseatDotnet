namespace BarberBoss.Infrastructure.DataAccess.Queries;
public static class DeleteBillingQuery
{
    public const string Sql = @"
        DELETE FROM billings
        WHERE id = @Id
    ";
}

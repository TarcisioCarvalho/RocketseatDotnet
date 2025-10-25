namespace BarberBoss.Infrastructure.DataAccess.Queries;
public static class GetAllBillings
{
    public const string Query = @"
        SELECT 
            id, 
            date,
            status
        FROM billings";
}

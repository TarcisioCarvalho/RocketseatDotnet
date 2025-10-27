namespace BarberBoss.Infrastructure.DataAccess.Queries;
public static class GetAllBillings
{
    public const string Query = @"
        SELECT 
            id, 
            date,
            status
        FROM billings
        ORDER BY date DESC
        LIMIT @PageSize
        OFFSET @OffSet";

    public const string CountQuery = @"
        SELECT 
            COUNT(*) 
        FROM billings";
}

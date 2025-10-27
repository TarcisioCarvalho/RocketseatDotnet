namespace BarberBoss.Infrastructure.DataAccess.Queries;
public static class GetAllBillings
{
    public const string Query = @"
        SELECT 
            id, 
            date,
            status
        FROM billings
        WHERE (@StartDate::DATE IS NULL OR date >= @StartDate::DATE) 
        AND (@EndDate::DATE IS NULL OR date <= @EndDate::DATE)
        ORDER BY date DESC
        LIMIT @PageSize
        OFFSET @OffSet";

    public const string CountQuery = @"
        SELECT 
            COUNT(*) 
        FROM billings";

    public const string SumTotalAmountQuery = @"
        SELECT 
            COALESCE(SUM(amount), 0) 
        FROM billings
         WHERE (@StartDate::DATE IS NULL OR date >= @StartDate::DATE) 
        AND (@EndDate::DATE IS NULL OR date <= @EndDate::DATE) ";
}

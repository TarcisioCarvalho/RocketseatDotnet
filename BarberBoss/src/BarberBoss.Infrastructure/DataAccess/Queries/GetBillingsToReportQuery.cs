namespace BarberBoss.Infrastructure.DataAccess.Queries;
public static class GetBillingsToReportQuery
{
    public const string Query = @"
        SELECT 
            service_name,
            date,
            payment_method,
            amount,
            notes
        FROM 
            billings 
        WHERE 
            (@StartDate::DATE IS NULL OR date >= @StartDate::DATE) AND
            (@EndDate::DATE IS NULL OR date <= @EndDate::DATE)
        ORDER BY 
            date DESC";
}
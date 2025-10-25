namespace BarberBoss.Infrastructure.DataAccess.Queries;
public static class GetBillingByIdQuery
{
    public const string Query = @"
        SELECT 
            id, 
            date, 
            barber_name, 
            client_name, 
            service_name, 
            amount, 
            payment_method, 
            status, 
            notes, 
            created_at,
            updated_at
        FROM billings
        WHERE id = @Id
    ";
}

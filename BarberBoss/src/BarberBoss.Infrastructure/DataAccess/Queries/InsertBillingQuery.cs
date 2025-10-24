namespace BarberBoss.Infrastructure.DataAccess.Queries;
public static class InsertBillingQuery
{
    public const string Query = @"
        INSERT INTO billings (
            id, 
            date, 
            barber_name, 
            client_name, 
            service_name, 
            amount, 
            payment_method, 
            status, 
            notes, 
            created_at
        )
        VALUES (
            @Id, 
            @Date, 
            @BarberName, 
            @ClientName, 
            @ServiceName, 
            @Amount, 
            @PaymentMethod::payment_method_enum,    
            @Status::status_enum,                    
            @Notes, 
            @CreatedAt
        )";
}

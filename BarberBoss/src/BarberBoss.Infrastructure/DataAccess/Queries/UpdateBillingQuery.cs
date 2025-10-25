namespace BarberBoss.Infrastructure.DataAccess.Queries;
public static class UpdateBillingQuery
{
    public const string Sql = @"
        UPDATE Billings
        SET 
            date = @Date, 
            barber_name = @BarberName, 
            client_name = @ClientName, 
            service_name = @ServiceName, 
            amount = @Amount, 
            payment_method = CAST(@PaymentMethod AS payment_method_enum),
            status = CAST(@Status AS status_enum),
            notes = @Notes, 
            updated_at = @UpdatedAt
        WHERE Id = @Id;
    ";
}
   
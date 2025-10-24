using BarberBoss.Domain.Enums;

namespace BarberBoss.Domain.DTOs;
public class BillingShort
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Status Status { get; set; }
}

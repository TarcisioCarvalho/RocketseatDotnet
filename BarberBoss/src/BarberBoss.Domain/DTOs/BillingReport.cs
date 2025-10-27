using BarberBoss.Domain.Enums;

namespace BarberBoss.Domain.DTOs;
public class BillingReport
{
    public string ServiceName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public string Notes { get; set; } = string.Empty;
}



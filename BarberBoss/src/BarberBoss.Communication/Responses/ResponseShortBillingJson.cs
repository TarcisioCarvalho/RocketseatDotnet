using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Responses;
public class ResponseShortBillingJson
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Status Status { get; set; }
}


   
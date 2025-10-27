namespace BarberBoss.Communication.Responses;
public class ResponseBillingsJson
{
    public List<ResponseShortBillingJson> Billings { get; set; } = [];
    public int Total { get; set; }
}

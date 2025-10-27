namespace BarberBoss.Communication.Requests;
public class RequestBillingsJson
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

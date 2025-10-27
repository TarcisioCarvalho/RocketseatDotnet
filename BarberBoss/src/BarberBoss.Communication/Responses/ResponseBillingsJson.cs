namespace BarberBoss.Communication.Responses;
public class ResponseBillingsJson
{
    public List<ResponseShortBillingJson> Billings { get; set; } = [];
    public int Total { get; set; }
    public int TotalPages { get; set; } 
    public int Page { get; set; }
    public int PageSize { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
    public decimal TotalAmount { get; set; }
}

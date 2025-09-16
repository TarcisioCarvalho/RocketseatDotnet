namespace GerenciadorLivraria.Communication.Requests;

public class RequestAddBookJson
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

}

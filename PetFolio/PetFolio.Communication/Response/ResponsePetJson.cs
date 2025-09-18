using PetFolio.Communication.Enums;

namespace PetFolio.Communication.Response;
public class ResponsePetJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public PetType PetType { get; set; }
}

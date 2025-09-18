using PetFolio.Communication.Enums;

namespace PetFolio.Communication.Response;
public class ResponseShortPetJson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PetType PetType { get; set; }
}

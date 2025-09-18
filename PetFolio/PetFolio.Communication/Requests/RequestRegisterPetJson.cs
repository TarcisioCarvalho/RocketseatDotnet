using PetFolio.Communication.Enums;

namespace PetFolio.Communication.Requests;
public class RequestRegisterPetJson
{
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public PetType  PetType { get; set; }
}

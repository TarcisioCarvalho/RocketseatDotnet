using PetFolio.Communication.Response;

namespace PetFolio.Application.UseCases.Pet.GetAll;
public class GetAllPetsUseCase
{
    public ResponseAllPetJson Execute()
    {
        return new ResponseAllPetJson()
        {
          Pets = new List<ResponseShortPetJson> { new ResponseShortPetJson() { 
          Id = 1,
          Name = "Test",
          PetType = 0,
          } }
        };
    }
}

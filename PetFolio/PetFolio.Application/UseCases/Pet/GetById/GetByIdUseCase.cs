using PetFolio.Communication.Response;

namespace PetFolio.Application.UseCases.Pet.GetById;
public class GetByIdUseCase
{
    public ResponsePetJson Execute(int id)
    {
        return new ResponsePetJson()
        {
            Id = id,
            BirthDate = new DateTime(year:2020, month:1, day: 1),
            Name = "Test",
            PetType = Communication.Enums.PetType.cat
        };
    }
}

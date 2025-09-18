using PetFolio.Communication.Requests;
using PetFolio.Communication.Response;

namespace PetFolio.Application.UseCases.Pet.Register;
public class RegisterPetUseCase
{
    public ResponseRegisterPetJson Execute(RequestRegisterPetJson request)
    {
        return new ResponseRegisterPetJson
        {
            Id=7,
            Name=request.Name,
        };
    }
}

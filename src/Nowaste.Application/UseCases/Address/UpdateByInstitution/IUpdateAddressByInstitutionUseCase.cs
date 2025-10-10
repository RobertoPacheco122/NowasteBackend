using Nowaste.Communication.Requests.Address;

namespace Nowaste.Application.UseCases.Address.UpdateByInstitution;

public interface IUpdateAddressByInstitutionUseCase {
    Task Execute(Guid institutionId, RequestRegisterAddressJson request);
}

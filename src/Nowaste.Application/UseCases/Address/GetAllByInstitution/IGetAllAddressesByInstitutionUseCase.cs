using Nowaste.Communication.Responses.Establishment;

namespace Nowaste.Application.UseCases.Address.GetAllByInstitution;

public interface IGetAllAddressesByInstitutionUseCase {
    Task<ICollection<ResponseGetAllAddressesJson>> Execute(Guid institutionId);
}

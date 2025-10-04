using Nowaste.Communication.Responses.Establishments;

namespace Nowaste.Application.UseCases.Address.GetAllByInstitution;

public interface IGetAllAddressesByInstitutionUseCase {
    Task<ICollection<ResponseGetAllAddressesJson>> Execute(Guid institutionId);
}

using Nowaste.Communication.Responses.Establishments;

namespace Nowaste.Application.UseCases.Address.GetAllByEstablishment;

public interface IGetAllAddressesByEstablishmentUseCase {
    Task<ICollection<ResponseGetAllAddressesJson>> Execute(Guid establishmentId);
}

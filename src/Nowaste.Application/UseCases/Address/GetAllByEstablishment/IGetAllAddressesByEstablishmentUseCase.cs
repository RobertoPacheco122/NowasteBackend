using Nowaste.Communication.Responses.Establishment;

namespace Nowaste.Application.UseCases.Address.GetAllByEstablishment;

public interface IGetAllAddressesByEstablishmentUseCase
{
    Task<ICollection<ResponseGetAllAddressesJson>> Execute(Guid establishmentId);
}

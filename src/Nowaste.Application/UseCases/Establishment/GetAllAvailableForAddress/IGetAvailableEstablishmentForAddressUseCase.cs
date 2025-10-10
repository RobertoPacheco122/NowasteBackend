using Nowaste.Communication.Responses.Establishment;

namespace Nowaste.Application.UseCases.Establishment.GetAllAvailableForAddress;

public interface IGetAvailableEstablishmentForAddressUseCase {
    Task<ICollection<ResponseGetAllAvailableEstablishmentForAddressJson>> Execute(Guid addressId);
}

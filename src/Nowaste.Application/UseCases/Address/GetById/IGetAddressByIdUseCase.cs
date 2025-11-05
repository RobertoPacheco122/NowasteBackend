using Nowaste.Communication.Responses.Address;

namespace Nowaste.Application.UseCases.Address.GetById;

public interface IGetAddressByIdUseCase
{
    Task<ResponseGetAddressByIdJson> Execute(Guid addressId);
}

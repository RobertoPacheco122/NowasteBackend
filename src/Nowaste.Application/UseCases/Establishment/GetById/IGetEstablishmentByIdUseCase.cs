using Nowaste.Communication.Responses.Establishment;

namespace Nowaste.Application.UseCases.Establishment.GetById;

public interface IGetEstablishmentByIdUseCase
{
    Task<ResponseGetEstablishmentByIdJson> Execute(Guid establishmentId);
}

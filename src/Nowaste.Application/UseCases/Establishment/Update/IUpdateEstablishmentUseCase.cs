using Nowaste.Communication.Requests.Establishment;

namespace Nowaste.Application.UseCases.Establishment.Update;

public interface IUpdateEstablishmentUseCase
{
    Task Execute(Guid establishmentId, RequestUpdateEstablishmentJson request);
}

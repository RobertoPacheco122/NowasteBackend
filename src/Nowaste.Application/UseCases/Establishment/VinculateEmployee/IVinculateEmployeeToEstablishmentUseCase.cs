using Nowaste.Communication.Requests.Establishment;

namespace Nowaste.Application.UseCases.Establishment.VinculateEmployee;

public interface IVinculateEmployeeToEstablishmentUseCase {
    Task Execute(RequestVinculateEmployeeToEstablishmentJson request);
}

using Nowaste.Communication.Requests.Establishment;

namespace Nowaste.Application.UseCases.Establishment.UpdateOperatingDay;

public interface IUpdateOperatingDayUseCase {
    Task Execute(Guid operatingDayId, RequestUpdateOperatingDayJson request);
}

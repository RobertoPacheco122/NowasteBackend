using Nowaste.Communication.Requests.Establishment;

namespace Nowaste.Application.UseCases.Establishment.RegisterOperatingDay;

public interface IRegisterOperatingDayUseCase {
    Task Execute(RequestRegisterOperatingDayJson request);
}

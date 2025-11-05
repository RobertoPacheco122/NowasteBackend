using Nowaste.Communication.Responses.Establishment;

namespace Nowaste.Application.UseCases.Establishment.Stats;

public interface IEstablishmentStatsUseCase
{
    Task<ResponseEstablishmentStatsJson> Execute(Guid id);
}

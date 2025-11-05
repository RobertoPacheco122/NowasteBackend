using Nowaste.Communication.Responses.Establishment;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Domain.Repositories.Order;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Establishment.Stats;

public class EstablishmentStatsUseCase(
    IOrderReadOnlyRepository orderReadOnlyRepository,
    IEstablishmentReadOnlyRepository establishmentReadOnlyRepository
) : IEstablishmentStatsUseCase
{
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository = orderReadOnlyRepository;
    private readonly IEstablishmentReadOnlyRepository _establishmentReadOnlyRepository =
        establishmentReadOnlyRepository;

    public async Task<ResponseEstablishmentStatsJson> Execute(Guid id)
    {
        var existActiveEstablishmentWithGivenId =
            await _establishmentReadOnlyRepository.ExistActiveWithId(id);

        if (!existActiveEstablishmentWithGivenId)
            throw new NotFoundException("Estabelecimento não encontrado.");

        var totalSales = await _orderReadOnlyRepository.GetTotalSalesByEstablishmentId(id);
        var totalReducedWasteInGrams =
            await _orderReadOnlyRepository.GetTotalWasteReducedByEstablishmentId(id);

        return new ResponseEstablishmentStatsJson
        {
            Sales = totalSales,
            ReducedWasteInGrams = totalReducedWasteInGrams,
        };
    }
}

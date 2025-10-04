using AutoMapper;
using Nowaste.Communication.Responses.Establishments;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Address.GetAllByEstablishment;

public class GetAllAddressesByEstablishmentUseCase(
        IMapper mapper,
        IAddressReadOnlyRepository addressReadOnlyRepository
    ) : IGetAllAddressesByEstablishmentUseCase {
    private readonly IMapper _mapper = mapper;
    private readonly IAddressReadOnlyRepository _addressReadOnlyRepository = addressReadOnlyRepository;

    public async Task<ICollection<ResponseGetAllAddressesJson>> Execute(Guid establishmentId) {
        var addresses = await _addressReadOnlyRepository.GetAllByEstablishment(establishmentId);

        if (addresses is null || addresses.Count == 0)
            throw new NotFoundException("Não foram encontrados endereços para este estabelecimento.");

        return _mapper.Map<ICollection<ResponseGetAllAddressesJson>>(addresses);
    }
}

using AutoMapper;
using Nowaste.Communication.Responses.Establishments;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Address.GetAllByPerson;

public class GetAllAddressesByPersonUseCase(
        IMapper mapper,
        IAddressReadOnlyRepository addressReadOnlyRepository
    ) : IGetAllAddressesByPersonUseCase {
    private readonly IMapper _mapper = mapper;
    private readonly IAddressReadOnlyRepository _addressReadOnlyRepository = addressReadOnlyRepository;

    public async Task<ICollection<ResponseGetAllAddressesJson>> Execute(Guid personId) {
        var addresses = await _addressReadOnlyRepository.GetAllByPerson(personId);

        if (addresses is null || addresses.Count == 0)
            throw new NotFoundException("Não foram encontrados endereços para esta pessoa.");

        return _mapper.Map<ICollection<ResponseGetAllAddressesJson>>(addresses);
    }
}

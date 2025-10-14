using AutoMapper;
using Nowaste.Communication.Responses.Establishment;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Address.GetAllByInstitution;

public class GetAllAddressesByInstitutionUseCase(
    IMapper mapper,
    IAddressReadOnlyRepository addressReadOnlyRepository
) : IGetAllAddressesByInstitutionUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IAddressReadOnlyRepository _addressReadOnlyRepository =
        addressReadOnlyRepository;

    public async Task<ICollection<ResponseGetAllAddressesJson>> Execute(Guid institutionId)
    {
        var addresses = await _addressReadOnlyRepository.GetAllByInstitution(institutionId);

        if (addresses is null || addresses.Count == 0)
            throw new NotFoundException("Não foram encontrados endereços para esta instituição.");

        return _mapper.Map<ICollection<ResponseGetAllAddressesJson>>(addresses);
    }
}

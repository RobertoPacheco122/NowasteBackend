using AutoMapper;
using Nowaste.Communication.Responses.Address;
using Nowaste.Domain.Repositories.Address;

namespace Nowaste.Application.UseCases.Address.GetById;

public class GetAddressByIdUseCase(
    IMapper mapper,
    IAddressReadOnlyRepository addressReadOnlyRepository
) : IGetAddressByIdUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IAddressReadOnlyRepository _addressReadOnlyRepository =
        addressReadOnlyRepository;

    public async Task<ResponseGetAddressByIdJson> Execute(Guid addressId)
    {
        var addressEntity =
            await _addressReadOnlyRepository.GetById(addressId)
            ?? throw new KeyNotFoundException("Endereço não encontrado");

        return _mapper.Map<ResponseGetAddressByIdJson>(addressEntity);
    }
}

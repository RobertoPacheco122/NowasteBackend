using AutoMapper;
using Nowaste.Communication.Responses.Establishment;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Establishment.GetById;

public class GetEstablishmentByIdUseCase(
        IMapper mapper,
        IEstablishmentReadOnlyRepository establishmentReadOnlyRepository
    ) : IGetEstablishmentByIdUseCase {
    private readonly IMapper _mapper = mapper;
    private readonly IEstablishmentReadOnlyRepository _establishmentReadOnlyRepository = establishmentReadOnlyRepository;

    public async Task<ResponseGetEstablishmentByIdJson> Execute(Guid establishmentId) {
        var establishmentEntity = await _establishmentReadOnlyRepository.GetById(establishmentId)
            ?? throw new NotFoundException("Estabelecimento não encontrado.");

        return _mapper.Map<ResponseGetEstablishmentByIdJson>(establishmentEntity);
    }
}

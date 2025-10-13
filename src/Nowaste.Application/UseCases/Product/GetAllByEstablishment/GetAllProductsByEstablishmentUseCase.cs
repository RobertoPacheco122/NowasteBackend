using AutoMapper;
using Nowaste.Communication.Responses.Product;
using Nowaste.Domain.Repositories.Product;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Product.GetAllByEstablishment;

public class GetAllProductsByEstablishmentUseCase(
    IMapper mapper,
    IProductReadOnlyRepository productReadOnlyRepository
) : IGetAllProductsByEstablishmentUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IProductReadOnlyRepository _productReadOnlyRepository =
        productReadOnlyRepository;

    public async Task<ICollection<ResponseGetAllProductsByEstablishmentJson>> Execute(
        Guid establishmentId
    )
    {
        var productsEntities = await _productReadOnlyRepository.GetAllByEstablishment(
            establishmentId
        );

        if (productsEntities.Count == 0)
            throw new NotFoundException("Produtos não encontrados.");

        return _mapper.Map<ICollection<ResponseGetAllProductsByEstablishmentJson>>(
            productsEntities
        );
    }
}

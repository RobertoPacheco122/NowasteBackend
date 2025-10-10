using AutoMapper;
using Nowaste.Communication.Responses.Product;
using Nowaste.Domain.Repositories.Product;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Product.GetAllCategories;

public class GetAllProductCategoriesUseCase(
    IMapper mapper,
    IProductReadOnlyRepository productReadOnlyRepository
    ) : IGetAllProductCategoriesUseCase {
    private readonly IMapper _mapper = mapper;
    private readonly IProductReadOnlyRepository _productReadOnlyRepository = productReadOnlyRepository;

    public async Task<ICollection<ResponseGetAllProductCategoriesJson>> Execute() {
        var productCategoriesEntities = await _productReadOnlyRepository.GetAllCategories();

        if(productCategoriesEntities.Count == 0)
            throw new NotFoundException("Nenhuma categoria de produto encontrada.");

        return _mapper.Map<ICollection<ResponseGetAllProductCategoriesJson>>(productCategoriesEntities);
    }
}

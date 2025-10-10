using AutoMapper;
using Nowaste.Communication.Requests.Product;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Product;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Product.UpdatePrice;

public class UpdateProductPriceUseCase(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IProductWriteOnlyRepository productWriteOnlyRepository,
    IProductReadOnlyRepository productReadOnlyRepository
    ) : IUpdateProductPriceUseCase {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IProductWriteOnlyRepository _productWriteOnlyRepository = productWriteOnlyRepository;
    private readonly IProductReadOnlyRepository _productReadOnlyRepository = productReadOnlyRepository;

    public async Task Execute(Guid productId, RequestUpdateProductPriceJson request) {
        Validate(request);

        var existActiveProductWithGivenId = await _productReadOnlyRepository
            .ExistActiveWithId(productId);

        if(existActiveProductWithGivenId is false)
            throw new NotFoundException("Produto não encontrado.");

        var productPriceHistoryEntity = _mapper.Map<ProductPriceHistoryEntity>(request);
        productPriceHistoryEntity.CreatedAt = DateTime.UtcNow;
        productPriceHistoryEntity.ProductId = productId;

        await _productWriteOnlyRepository.AddPrice(productPriceHistoryEntity);

        await _unitOfWork.Commit();
    }

    private static void Validate(RequestUpdateProductPriceJson request) {
        var validationResult = new UpdateProductPriceValidator().Validate(request);

        if (validationResult.IsValid is false) {
            var errorsMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}

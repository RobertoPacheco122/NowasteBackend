using Nowaste.Communication.Requests.Product;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Product;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Product.Update;

public class UpdateProductUseCase(
        IUnitOfWork unitOfWork,
        IProductReadOnlyRepository productReadOnlyRepository,
        IProductUpdateOnlyRepository productUpdateOnlyRepository
    ) : IUpdateProductUseCase {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductReadOnlyRepository _productReadOnlyRepository = productReadOnlyRepository;
    private readonly IProductUpdateOnlyRepository _productUpdateOnlyRepository = productUpdateOnlyRepository;

    public async Task Execute(Guid productId, RequestUpdateProductJson request) {
        await Validate(request);

        var productEntity = await _productUpdateOnlyRepository.GetById(productId) ??
            throw new NotFoundException("Produto não encontrado.");

        productEntity.Name = request.Name;
        productEntity.Description = request.Description;
        productEntity.ProductCategoryId = request.ProductCategoryId;

        _productUpdateOnlyRepository.Update(productEntity);

        await _unitOfWork.Commit();
    }

    public async Task Validate(RequestUpdateProductJson request) {
        var validationResult = new UpdateProductValidator().Validate(request);

        var existActiveProductCategoryWithGivenId = await _productReadOnlyRepository
            .ExistActiveCategoryWithId(request.ProductCategoryId);

        if (existActiveProductCategoryWithGivenId is false)
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                string.Empty,
                "A categoria informada não existe.")
            );

        if (validationResult.IsValid is false) {
            var errorsMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}

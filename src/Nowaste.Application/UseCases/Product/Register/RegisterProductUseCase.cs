using AutoMapper;
using Nowaste.Communication.Requests.Product;
using Nowaste.Communication.Responses.Product;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Domain.Repositories.Product;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Product.Register;

public class RegisterProductUseCase(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IProductWriteOnlyRepository productWriteOnlyRepository,
        IProductReadOnlyRepository productReadOnlyRepository,
        IEstablishmentReadOnlyRepository establishmentReadOnlyRepository
    ) : IRegisterProductUseCase {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IProductWriteOnlyRepository _productWriteOnlyRepository = productWriteOnlyRepository;
    private readonly IProductReadOnlyRepository _productReadOnlyRepository = productReadOnlyRepository;
    private readonly IEstablishmentReadOnlyRepository _establishmentReadOnlyRepository = establishmentReadOnlyRepository;

    public async Task<ResponseRegisteredProductJson> Execute(RequestRegisterProductJson request) {
        await Validate(request);

        var productEntity = _mapper.Map<ProductEntity>(request);
        productEntity.CreatedAt = DateTime.UtcNow;

        await _productWriteOnlyRepository.AddProduct(productEntity);

        var productPriceHistoryEntity = new ProductPriceHistoryEntity {
            CreatedAt = DateTime.UtcNow,
            ProductId = productEntity.Id,
            Price = request.Price,
            EffectiveDate = DateTime.UtcNow,
            SalePrice = request.Price,
        };

        await _productWriteOnlyRepository.AddPrice(productPriceHistoryEntity);

        await _unitOfWork.Commit();

        return new ResponseRegisteredProductJson {
            Id = productEntity.Id,
            Name = productEntity.Name,
            Description = productEntity.Description,
            Price = productPriceHistoryEntity.Price,
        };
    }

    public async Task Validate(RequestRegisterProductJson request) {
        var validationResult = new RegisterProductValidator().Validate(request);

        var existActiveProductCategoryWithGivenId = await _productReadOnlyRepository
            .ExistActiveCategoryWithId(request.ProductCategoryId);

        if (existActiveProductCategoryWithGivenId is false)
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                string.Empty,
                "A categoria informada não existe.")
            );
        
        var existActiveEstablishmentWithGivenId = await _establishmentReadOnlyRepository
            .ExistActiveWithId(request.EstablishmentId);

        if (existActiveEstablishmentWithGivenId is false)
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                string.Empty,
                "O estabelecimento informado não existe.")
            );

        if (validationResult.IsValid is false) {
            var errorsMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}

using AutoMapper;
using Nowaste.Communication.Requests.Product;
using Nowaste.Communication.Responses.Product;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Product;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Product.RegisterCategory;

public class RegisterProductCategoryUseCase(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IProductWriteOnlyRepository productWriteOnlyRepository
) : IRegisterProductCategoryUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IProductWriteOnlyRepository _productWriteOnlyRepository =
        productWriteOnlyRepository;

    public async Task<ResponseRegisteredProductCategoryJson> Execute(
        RequestRegisterProductCategoryJson request
    )
    {
        Validate(request);

        var productCategoryEntity = _mapper.Map<ProductCategoryEntity>(request);
        productCategoryEntity.CreatedAt = DateTime.UtcNow;

        await _productWriteOnlyRepository.AddProductCategory(productCategoryEntity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredProductCategoryJson>(productCategoryEntity);
    }

    public static void Validate(RequestRegisterProductCategoryJson request)
    {
        var validationResult = new RegisterProductCategoryValidator().Validate(request);

        if (validationResult.IsValid is false)
        {
            var errorsMessages = validationResult
                .Errors.Select(error => error.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
        ;
    }
}

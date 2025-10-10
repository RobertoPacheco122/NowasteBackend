
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Product;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Product.ToggleIsActive;

public class ToggleIsProductActiveUseCase(
        IUnitOfWork unitOfWork,
        IProductUpdateOnlyRepository productUpdateOnlyRepository
    ) : IToggleIsProductActiveUseCase {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductUpdateOnlyRepository _productUpdateOnlyRepository = productUpdateOnlyRepository;

    public async Task Execute(Guid productId) {
        var productEntity = await _productUpdateOnlyRepository.GetById(productId) ??
            throw new NotFoundException("Produto não encontrado.");

        productEntity.IsActive = !productEntity.IsActive;

        _productUpdateOnlyRepository.Update(productEntity);

        await _unitOfWork.Commit();
    }
}

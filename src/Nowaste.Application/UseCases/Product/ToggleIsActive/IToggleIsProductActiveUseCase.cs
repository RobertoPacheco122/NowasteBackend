namespace Nowaste.Application.UseCases.Product.ToggleIsActive;

public interface IToggleIsProductActiveUseCase {
    Task Execute(Guid productId);
}

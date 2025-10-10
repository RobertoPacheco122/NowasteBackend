using Nowaste.Communication.Requests.Product;

namespace Nowaste.Application.UseCases.Product.Update;

public interface IUpdateProductUseCase {
    Task Execute(Guid productId, RequestUpdateProductJson request);
}

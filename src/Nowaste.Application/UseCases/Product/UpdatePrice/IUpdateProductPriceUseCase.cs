using Nowaste.Communication.Requests.Product;

namespace Nowaste.Application.UseCases.Product.UpdatePrice;

public interface IUpdateProductPriceUseCase
{
    Task Execute(Guid productId, RequestUpdateProductPriceJson request);
}

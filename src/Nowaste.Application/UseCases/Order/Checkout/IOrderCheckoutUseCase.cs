using Nowaste.Communication.Requests.Order;
using Nowaste.Communication.Responses.Order;

namespace Nowaste.Application.UseCases.Order.Checkout;

public interface IOrderCheckoutUseCase
{
    Task<ResponseOrderCheckoutJson> Execute(RequestOrderCheckoutJson request);
}

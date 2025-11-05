using Nowaste.Communication.Enums;
using Nowaste.Communication.Requests.Order;

namespace Nowaste.Application.UseCases.Order.UpdateStatus;

public interface IUpdateOrderStatusUseCase
{
    Task Execute(Guid orderId, RequestUpdateOrderStatusJson request);
}

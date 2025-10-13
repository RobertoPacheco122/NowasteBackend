using Nowaste.Communication.Responses.Order;

namespace Nowaste.Application.UseCases.Order.GetById;

public interface IGetOrderByIdUseCase
{
    Task<ResponseGetOrderByIdJson> Execute(Guid orderId);
}

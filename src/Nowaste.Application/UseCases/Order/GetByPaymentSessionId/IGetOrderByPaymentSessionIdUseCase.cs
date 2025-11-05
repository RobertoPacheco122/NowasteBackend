using Nowaste.Communication.Responses.Order;

namespace Nowaste.Application.UseCases.Order.GetByPaymentSessionId;

public interface IGetOrderByPaymentSessionIdUseCase
{
    Task<ResponseGetOrderByIdJson> Execute(string paymentSessionId);
}

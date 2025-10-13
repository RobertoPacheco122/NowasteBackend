using Nowaste.Communication.Requests.Order;
using Nowaste.Communication.Responses.Order;

namespace Nowaste.Application.UseCases.Order.Register;

public interface IRegisterOrderUseCase {
    Task<ResponseRegisteredOrderJson> Execute(RequestRegisterOrderJson request);
}

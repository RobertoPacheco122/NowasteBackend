using Nowaste.Communication.Requests.Product;
using Nowaste.Communication.Responses.Product;

namespace Nowaste.Application.UseCases.Product.Register;

public interface IRegisterProductUseCase {
    Task<ResponseRegisteredProductJson> Execute(RequestRegisterProductJson request);
}

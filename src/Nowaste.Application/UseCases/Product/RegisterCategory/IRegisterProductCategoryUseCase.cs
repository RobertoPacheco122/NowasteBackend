using Nowaste.Communication.Requests.Product;
using Nowaste.Communication.Responses.Product;

namespace Nowaste.Application.UseCases.Product.RegisterCategory;

public interface IRegisterProductCategoryUseCase {
    Task<ResponseRegisteredProductCategoryJson> Execute(RequestRegisterProductCategoryJson request);
}

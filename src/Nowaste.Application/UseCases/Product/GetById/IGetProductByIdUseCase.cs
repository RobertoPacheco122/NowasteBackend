using Nowaste.Communication.Responses.Product;

namespace Nowaste.Application.UseCases.Product.GetById;

public interface IGetProductByIdUseCase
{
    Task<ResponseGetProductByIdJson> Execute(Guid id);
}

using Nowaste.Communication.Responses.Product;

namespace Nowaste.Application.UseCases.Product.GetAllCategories;
public interface IGetAllProductCategoriesUseCase {
    Task<ICollection<ResponseGetAllProductCategoriesJson>> Execute();
}

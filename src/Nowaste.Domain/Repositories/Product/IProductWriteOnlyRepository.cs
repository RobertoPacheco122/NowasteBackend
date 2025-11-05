using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Product;

public interface IProductWriteOnlyRepository
{
    Task AddProduct(ProductEntity product);
    Task AddProductCategory(ProductCategoryEntity productCategory);
    Task AddPrice(ProductPriceHistoryEntity priceHistory);
}

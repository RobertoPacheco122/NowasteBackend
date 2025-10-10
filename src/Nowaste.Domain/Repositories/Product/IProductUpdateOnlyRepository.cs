using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Product;

public interface IProductUpdateOnlyRepository {
    Task<ProductEntity?> GetById(Guid id);
    void Update(ProductEntity product);
}

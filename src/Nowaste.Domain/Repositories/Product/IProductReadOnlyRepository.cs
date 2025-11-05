using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Product;

public interface IProductReadOnlyRepository
{
    Task<bool> ExistActiveCategoryWithId(Guid categoryId);
    Task<bool> ExistActiveWithId(Guid id);
    Task<ICollection<ProductEntity>> GetAllByEstablishment(Guid establishmentId);
    Task<ICollection<ProductEntity>> GetAllByIds(ICollection<Guid> ids);
    Task<ICollection<ProductCategoryEntity>> GetAllCategories();
    Task<ProductEntity?> GetById(Guid id);
}

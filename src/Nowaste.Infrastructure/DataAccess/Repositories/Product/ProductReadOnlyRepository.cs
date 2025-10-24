using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Product;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Product;

internal class ProductReadOnlyRepository(NowasteDbContext dbContext) : IProductReadOnlyRepository
{
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<bool> ExistActiveCategoryWithId(Guid categoryId)
    {
        return await _dbContext
            .ProductCategories.AsNoTracking()
            .AnyAsync(category => category.Id == categoryId);
    }

    public async Task<bool> ExistActiveWithId(Guid id)
    {
        return await _dbContext.Products.AsNoTracking().AnyAsync(category => category.Id == id);
    }

    public async Task<ICollection<ProductEntity>> GetAllByEstablishment(Guid establishmentId)
    {
        return await _dbContext
            .Products.AsNoTracking()
            .Include(product => product.PriceHistories)
            .Include(product => product.ProductCategory)
            .Where(product => product.EstablishmentId == establishmentId)
            .ToListAsync();
    }

    public async Task<ICollection<ProductEntity>> GetAllByIds(ICollection<Guid> ids)
    {
        return await _dbContext
            .Products.AsNoTracking()
            .Include(product => product.PriceHistories)
            .Include(product => product.ProductCategory)
            .Where(product => ids.Contains(product.Id))
            .ToListAsync();
    }

    public async Task<ICollection<ProductCategoryEntity>> GetAllCategories()
    {
        return await _dbContext.ProductCategories.AsNoTracking().ToListAsync();
    }

    public async Task<ProductEntity?> GetById(Guid id)
    {
        return await _dbContext
            .Products.AsNoTracking()
            .Include(product => product.PriceHistories)
            .Include(product => product.ProductCategory)
            .Include(product => product.Establishment)
            .ThenInclude(establishment => establishment.Reviews)
            .FirstOrDefaultAsync(product => product.Id == id);
    }
}

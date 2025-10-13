using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Product;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Product;

internal class ProductUpdateOnlyRepository(NowasteDbContext dbContext)
    : IProductUpdateOnlyRepository
{
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<ProductEntity?> GetById(Guid id)
    {
        return await _dbContext
            .Products.Include(product => product.PriceHistories)
            .Include(product => product.ProductCategory)
            .FirstOrDefaultAsync(product => product.Id == id);
    }

    public void Update(ProductEntity product)
    {
        _dbContext.Products.Update(product);
    }
}

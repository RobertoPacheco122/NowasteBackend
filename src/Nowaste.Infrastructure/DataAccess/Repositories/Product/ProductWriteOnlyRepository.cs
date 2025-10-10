using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Product;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Product;

internal class ProductWriteOnlyRepository(NowasteDbContext dbContext) : IProductWriteOnlyRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task AddPrice(ProductPriceHistoryEntity priceHistory) {
        await _dbContext
            .ProductPricesHistory
            .AddAsync(priceHistory);
    }

    public async Task AddProduct(ProductEntity product) {
        await _dbContext
            .Products
            .AddAsync(product);
    }

    public async Task AddProductCategory(ProductCategoryEntity productCategory) {
        await _dbContext
            .ProductCategories
            .AddAsync(productCategory);
    }
}

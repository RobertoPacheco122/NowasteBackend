namespace Nowaste.Domain.Entities;

public class ProductPriceHistoryEntity : BaseEntity {
    public required decimal Price { get; set; }
    public required decimal SalePrice { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public required DateTime EffectiveDate { get; set; }

    public required Guid ProductId { get; set; }
    public virtual required ProductEntity Product { get; set; }
}

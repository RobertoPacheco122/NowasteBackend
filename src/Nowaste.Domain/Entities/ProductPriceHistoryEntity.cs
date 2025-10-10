namespace Nowaste.Domain.Entities;

public class ProductPriceHistoryEntity : BaseEntity {
    public required int Price { get; set; }
    public required int SalePrice { get; set; }
    public bool ShowDiscountAsPercentage { get; set; } = true;
    public required DateTime EffectiveDate { get; set; }

    public required Guid ProductId { get; set; }
    public virtual ProductEntity Product { get; set; } = null!;
}

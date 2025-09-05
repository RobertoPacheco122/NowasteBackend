using Nowaste.Domain.Enums;

namespace Nowaste.Domain.Entities;

public class ProductEntity : BaseEntity {
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal? QuantityInStock { get; set; }
    public bool IsActive { get; set; } = true;
    public EProductInventoryTrackingType InventoryTrackingType { get; set; }

    public Guid ProductCategoryId { get; set; }
    public virtual required ProductCategoryEntity ProductCategory { get; set; }
    public Guid EstablishmentId { get; set; }
    public virtual required EstablishmentEntity Establishment { get; set; }
    public ICollection<ProductPriceHistoryEntity>? PriceHistories { get; set; }
}

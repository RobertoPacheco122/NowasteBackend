namespace Nowaste.Domain.Entities;

public class ProductCategoryEntity : BaseEntity {
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;

    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
}
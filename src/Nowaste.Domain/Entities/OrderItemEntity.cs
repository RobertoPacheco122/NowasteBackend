namespace Nowaste.Domain.Entities;

public class OrderItemEntity : BaseEntity
{
    public required string ProductName { get; set; }
    public required int UnitPrice { get; set; }
    public required int ItemQuantity { get; set; }

    public required int Subtotal { get; set; }
    public int Discount { get; set; }
    public required int Total { get; set; }

    public required Guid OrderId { get; set; }
    public OrderEntity Order { get; set; } = null!;
    public required Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = null!;
}

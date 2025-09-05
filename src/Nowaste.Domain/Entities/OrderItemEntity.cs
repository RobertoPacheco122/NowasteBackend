namespace Nowaste.Domain.Entities;

public class OrderItemEntity : BaseEntity {
    public required string ProductName { get; set; }
    public required decimal UnitPrice { get; set; }
    public required int ItemQuantity { get; set; }

    public required decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public required decimal Total { get; set; }

    public Guid OrderId { get; set; }
    public required OrderEntity Order { get; set; }
}
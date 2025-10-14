using Nowaste.Domain.Enums;

namespace Nowaste.Domain.Entities;

public class OrderEntity : BaseEntity
{
    public required string FriendlyId { get; set; } = string.Empty;
    public required DateTime OrderDate { get; set; }
    public EOrderStatus OrderStatus { get; set; }
    public EPaymentMethod PaymentMethod { get; set; }
    public bool IsPaid { get; set; }
    public required string DeliveryAddress { get; set; }
    public string Observations { get; set; } = string.Empty;

    public required int Subtotal { get; set; }
    public required int DeliveryFee { get; set; }
    public required int Tax { get; set; }
    public int Discount { get; set; }
    public required int Total { get; set; }

    public Guid PersonId { get; set; }
    public required PersonEntity Person { get; set; }
    public Guid EstablishmentId { get; set; }
    public required EstablishmentEntity Establishment { get; set; }
    public ICollection<OrderItemEntity> OrderItems { get; set; } = [];
}

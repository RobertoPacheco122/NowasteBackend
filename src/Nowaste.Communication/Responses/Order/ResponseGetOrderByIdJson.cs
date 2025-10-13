using Nowaste.Communication.Enums;
using Nowaste.Communication.Responses.Establishment;
using Nowaste.Communication.Responses.Users;

namespace Nowaste.Communication.Responses.Order;

public class ResponseGetOrderByIdJson {
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

    public ResponseGetEstablishmentByIdJson Establishment { get; set; } = null!;
    public ResponseGetPersonByIdJson Person { get; set; } = null!;
    public ICollection<ResponseGetOrderItemByIdJson> OrderItems { get; set; } = [];
}

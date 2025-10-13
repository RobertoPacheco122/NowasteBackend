using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Responses.Order;

public class ResponseRegisteredOrderJson {
    public Guid OrderId { get; set; }
    public EOrderStatus OrderStatus { get; set; }
}

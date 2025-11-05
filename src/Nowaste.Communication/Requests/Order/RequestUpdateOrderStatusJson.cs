using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Requests.Order;

public class RequestUpdateOrderStatusJson
{
    public EOrderStatus Status { get; set; }
}

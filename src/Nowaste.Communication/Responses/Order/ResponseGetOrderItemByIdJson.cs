using Nowaste.Communication.Responses.Product;

namespace Nowaste.Communication.Responses.Order;

public class ResponseGetOrderItemByIdJson
{
    public string ProductName { get; set; } = string.Empty;
    public int UnitPrice { get; set; }
    public int ItemQuantity { get; set; }
    public int Subtotal { get; set; }
    public int Discount { get; set; }
    public int Total { get; set; }
    public Guid OrderId { get; set; }
    public ResponseGetProductByIdJson Product { get; set; } = null!;
}

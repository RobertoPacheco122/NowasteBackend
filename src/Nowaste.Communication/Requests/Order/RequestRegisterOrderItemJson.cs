namespace Nowaste.Communication.Requests.Order;

public class RequestRegisterOrderItemJson {
    public Guid ProductId { get; set; }
    public int ItemQuantity { get; set; }
}

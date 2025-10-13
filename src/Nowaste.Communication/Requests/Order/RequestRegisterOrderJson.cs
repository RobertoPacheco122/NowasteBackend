using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Requests.Order;

public class RequestRegisterOrderJson
{
    public Guid AddressId { get; set; }
    public Guid EstablishmentId { get; set; }
    public ICollection<RequestRegisterOrderItemJson> Items { get; set; } = [];

    public DateTime OrderDate { get; set; }
    public EPaymentMethod PaymentMethod { get; set; }
    public string Observations { get; set; } = string.Empty;
}

using Nowaste.Communication.Enums;
using Nowaste.Communication.Responses.Establishment;

namespace Nowaste.Communication.Responses.Product;

public class ResponseGetProductByIdJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? QuantityInStock { get; set; }
    public bool IsActive { get; set; }
    public EProductInventoryTrackingType InventoryTrackingType { get; set; }
    public ResponseGetEstablishmentByIdJson Establishment { get; set; } = new();
    public ResponseGetProductPriceByIdJson ActualPriceHistory { get; set; } = new();
}

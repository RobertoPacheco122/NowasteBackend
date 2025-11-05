using Nowaste.Communication.Enums;
using Nowaste.Communication.Responses.Product;

namespace Nowaste.Communication.Responses.Establishment;

public class ResponseAvailableEstablishmentProductJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? QuantityInStock { get; set; }
    public bool IsActive { get; set; }
    public EProductInventoryTrackingType InventoryTrackingType { get; set; }
    public ResponseGetProductCategoryByIdJson ProductCategory { get; set; } = new();
    public ResponseGetProductPriceByIdJson ActualPriceHistory { get; set; } = new();
}

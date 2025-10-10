using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Responses.Product;

public class ResponseGetAllProductsByEstablishmentJson {
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal? QuantityInStock { get; set; }
    public bool IsActive { get; set; } = true;
    public EProductInventoryTrackingType InventoryTrackingType { get; set; }
    public ResponseGetProductCategoryByIdJson ProductCategory { get; set; } = new();
    public ResponseGetProductPriceByIdJson ActualPriceHistory { get; set; } = new();
}

using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Requests.Product;

public class RequestUpdateProductJson
{
    public string Name { get; set; } = string.Empty;
    public int WeightInGrams { get; set; }
    public string Description { get; set; } = string.Empty;
    public EProductInventoryTrackingType InventoryTrackingType { get; set; }
    public int? QuantityInStock { get; set; }
    public Guid ProductCategoryId { get; set; }
}

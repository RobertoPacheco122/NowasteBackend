using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Requests.Product;

public class RequestRegisterProductJson
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? QuantityInStock { get; set; }
    public bool IsActive { get; set; } = true;
    public int WeightInGrams { get; set; }
    public EProductInventoryTrackingType InventoryTrackingType { get; set; }
    public Guid ProductCategoryId { get; set; }
    public Guid EstablishmentId { get; set; }
    public int Price { get; set; }
}

namespace Nowaste.Communication.Requests.Product;

public class RequestUpdateProductPriceJson
{
    public required int Price { get; set; }
    public required int SalePrice { get; set; }
    public bool ShowDiscountAsPercentage { get; set; }
    public required DateTime EffectiveDate { get; set; }
}

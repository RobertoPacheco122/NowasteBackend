namespace Nowaste.Communication.Responses.Product;

public class ResponseGetProductPriceByIdJson
{
    public Guid Id { get; set; }
    public int Price { get; set; }
    public int SalePrice { get; set; }
    public bool ShowDiscountAsPercentage { get; set; }
    public DateTime EffectiveDate { get; set; }
    public Guid ProductId { get; set; }
}

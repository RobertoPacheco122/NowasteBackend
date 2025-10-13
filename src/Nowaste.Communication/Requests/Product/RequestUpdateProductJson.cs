namespace Nowaste.Communication.Requests.Product;

public class RequestUpdateProductJson
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? QuantityInStock { get; set; }
    public Guid ProductCategoryId { get; set; }
}

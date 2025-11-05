namespace Nowaste.Communication.Requests.Review;

public class RequestRegisterReviewJson
{
    public required int Rating { get; set; }
    public string PersonComment { get; set; } = string.Empty;
    public required Guid OrderId { get; set; }
}

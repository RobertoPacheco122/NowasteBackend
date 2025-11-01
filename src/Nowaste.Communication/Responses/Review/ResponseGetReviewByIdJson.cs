using Nowaste.Communication.Responses.Users;

namespace Nowaste.Communication.Responses.Review;

public class ResponseGetReviewByIdJson
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string PersonComment { get; set; } = string.Empty;
    public string EstablishmentResponse { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }
    public DateTime? ResponseDate { get; set; }

    public ResponseGetPersonByIdJson Person { get; set; } = new();
    public Guid OrderId { get; set; }
    public Guid EstablishmentId { get; set; }
}

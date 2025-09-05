namespace Nowaste.Domain.Entities;

public class ReviewEntity : BaseEntity {
    public required int Rating { get; set; }
    public string PersonComment { get; set; } = string.Empty;
    public string EstablishmentResponse { get; set; } = string.Empty;
    public required DateTime ReviewDate { get; set; }
    public DateTime? ResponseDate { get; set; }

    public Guid PersonId { get; set; }
    public required PersonEntity Person { get; set; }
    public Guid EstablishmentId { get; set; }
    public required EstablishmentEntity Establishment { get; set; }
}
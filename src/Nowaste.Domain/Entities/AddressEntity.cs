namespace Nowaste.Domain.Entities;

public class AddressEntity : BaseEntity {
    public required string StreetName { get; set; }
    public required string Number { get; set; }
    public string Complement { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Guid? PersonId { get; set; }
    public virtual PersonEntity? Person { get; set; }
    public Guid? EstablishmentId { get; set; }
    public virtual EstablishmentEntity? Establishment { get; set; }
    public Guid? InstitutionId { get; set; }
    public virtual InstitutionEntity? Institution { get; set; }
}

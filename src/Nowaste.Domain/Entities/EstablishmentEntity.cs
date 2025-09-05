using Nowaste.Domain.Enums;

namespace Nowaste.Domain.Entities;

public class EstablishmentEntity : BaseEntity {
    public required string ExhibitionName { get; set; }
    public required EEstablishmentType EstablishmentType { get; set; }
    public required string Email { get; set; }
    public string Telephone { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public EEstablishmentStatus Status { get; set; }

    public string? ResponsibleCpf { get; set; }
    public string? ResponsibleName { get; set; }

    public string? Cnpj { get; set; }
    public string? LegalName { get; set; }
    public string? TradeName { get; set; }

    public Guid AddressId { get; set; }
    public virtual ICollection<AddressEntity>? Addresses { get; set; }
    public virtual ICollection<PersonEntity>? Persons { get; set; }
    public virtual ICollection<ReviewEntity>? Reviews { get; set; }
}

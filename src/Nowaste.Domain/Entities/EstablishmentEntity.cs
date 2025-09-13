using Nowaste.Domain.Enums;

namespace Nowaste.Domain.Entities;

public class EstablishmentEntity : BaseEntity {
    public required string Cnpj { get; set; }
    public required string LegalName { get; set; }
    public required string TradeName { get; set; }
    public required string ExhibitionName { get; set; }
    public required string Email { get; set; }
    public string Telephone { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public EEstablishmentStatus Status { get; set; } = EEstablishmentStatus.AwaitingApproval;

    public virtual ICollection<AddressEntity> Addresses { get; set; } = [];
    public virtual ICollection<PersonEntity> Persons { get; set; } = [];
    public virtual ICollection<ReviewEntity> Reviews { get; set; } = [];
}

namespace Nowaste.Domain.Entities;

public class InstitutionEntity : BaseEntity {
    public required string ExhibitionName { get; set; }
    public required string Email { get; set; }
    public required string Cnpj { get; set; }
    public required string LegalName { get; set; }
    public required string TradeName { get; set; }
    public string Telephone { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public Guid AddressId { get; set; }
    public virtual ICollection<AddressEntity>? Addresses { get; set; }
    public virtual ICollection<PersonEntity>? Persons { get; set; }
}

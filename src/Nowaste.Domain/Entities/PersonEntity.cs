using Nowaste.Domain.Enums;

namespace Nowaste.Domain.Entities;

public class PersonEntity : BaseEntity {
    public required string FullName { get; set; }
    public required string Cpf { get; set; }
    public required DateOnly BirthDate { get; set; }
    public required string PhoneNumber { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;

    public Guid UserId { get; set; }
    public virtual UserEntity? User { get; set; }
    public Guid? EstablishmentId { get; set; }
    public virtual EstablishmentEntity? Establishment { get; set; }
    public Guid? InstitutionId { get; set; }
    public virtual InstitutionEntity? Institution { get; set; }
    public virtual ICollection<AddressEntity>? Addresses { get; set; }
}

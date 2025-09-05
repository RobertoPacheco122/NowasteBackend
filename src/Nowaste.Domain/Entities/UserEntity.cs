using Nowaste.Domain.Enums;

namespace Nowaste.Domain.Entities;

public class UserEntity : BaseEntity {
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public EUserStatus UserStatus { get; set; }

    public virtual required PersonEntity Person { get; set; }
    public string Role { get; set; } = string.Empty;
}

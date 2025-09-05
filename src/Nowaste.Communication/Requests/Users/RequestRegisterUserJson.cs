using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Requests.Users;

public class RequestRegisterUserJson {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string? Nickname { get; set; }

    public Guid? EstablishmentId { get; set; }
    public Guid? InstitutionId { get; set; }
}

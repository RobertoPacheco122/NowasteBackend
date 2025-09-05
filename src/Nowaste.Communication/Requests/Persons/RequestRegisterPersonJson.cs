using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Requests.Persons;
public class RequestRegisterPersonJson {
    public string FullName { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;

    public Guid UserId { get; set; }
    public Guid? EstablishmentId { get; set; }
    public Guid? InstitutionId { get; set; }
}

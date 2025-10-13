using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Requests.Users;

public class RequestUpdateUserProfileJson
{
    public string Nickname { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public EUserStatus UserStatus { get; set; }

    public Guid? EstablishmentId { get; set; }
    public Guid? InstitutionId { get; set; }
}

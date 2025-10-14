using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Requests.Establishment;

public class RequestUpdateEstablishmentJson
{
    public string ExhibitionName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public EEstablishmentStatus Status { get; set; }
}

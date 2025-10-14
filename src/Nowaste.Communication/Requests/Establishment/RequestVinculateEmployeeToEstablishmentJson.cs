namespace Nowaste.Communication.Requests.Establishment;

public class RequestVinculateEmployeeToEstablishmentJson
{
    public Guid UserId { get; set; }
    public Guid EstablishmentId { get; set; }
    public string Role { get; set; } = string.Empty;
}

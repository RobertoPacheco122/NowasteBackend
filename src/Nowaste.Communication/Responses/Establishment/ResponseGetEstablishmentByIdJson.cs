using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Responses.Establishment;

public class ResponseGetEstablishmentByIdJson
{
    public Guid Id { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    public string LegalName { get; set; } = string.Empty;
    public string TradeName { get; set; } = string.Empty;
    public string ExhibitionName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public double? ServiceRadiusInMeters { get; set; }
    public double AverageRating { get; set; }
    public EEstablishmentStatus Status { get; set; }
}

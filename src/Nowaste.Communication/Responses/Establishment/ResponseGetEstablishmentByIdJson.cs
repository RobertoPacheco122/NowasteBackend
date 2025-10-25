using Nowaste.Communication.Enums;
using Nowaste.Communication.Responses.Address;

namespace Nowaste.Communication.Responses.Establishment;

public class ResponseGetEstablishmentByIdJson
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    public string LegalName { get; set; } = string.Empty;
    public string TradeName { get; set; } = string.Empty;
    public string ExhibitionName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public double? ServiceRadiusInMeters { get; set; }
    public int TotalReviews { get; set; }
    public double AverageRating { get; set; }
    public ResponseGetAddressByIdJson OperationalAddress { get; set; } = new();
    public EEstablishmentStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
}

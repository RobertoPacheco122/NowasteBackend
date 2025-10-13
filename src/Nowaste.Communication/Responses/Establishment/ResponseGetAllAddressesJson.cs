using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Responses.Establishment;

public class ResponseGetAllAddressesJson
{
    public required string StreetName { get; set; }
    public required string Number { get; set; }
    public string Complement { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public EAddressType AddressType { get; set; }

    public Guid? PersonId { get; set; }
    public Guid? EstablishmentId { get; set; }
    public Guid? InstitutionId { get; set; }
}

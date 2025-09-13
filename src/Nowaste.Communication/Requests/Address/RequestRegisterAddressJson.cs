using Nowaste.Communication.Enums;

namespace Nowaste.Communication.Requests.Address;

public class RequestRegisterAddressJson {
    public string StreetName { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public EAddressType AddressType { get; set; } = EAddressType.Home;

    public Guid? PersonId { get; set; }
    public Guid? EstablishmentId { get; set; }
    public Guid? InstitutionId { get; set; }
}

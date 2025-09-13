using Nowaste.Communication.Requests.Address;

namespace Nowaste.Communication.Requests.Establishments;

public class RequestRegisterEstablishmentJson {
    public string Cnpj { get; set; } = string.Empty;
    public string LegalName { get; set; } = string.Empty;
    public string TradeName { get; set; } = string.Empty;
    public string ExhibitionName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public required RequestRegisterAddressJson OperationAddress { get; set; }
}

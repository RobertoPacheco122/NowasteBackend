using Nowaste.Communication.Responses.Product;

namespace Nowaste.Communication.Responses.Establishment;

public class ResponseGetAllAvailableEstablishmentForAddressJson
{
    public ResponseGetEstablishmentByIdJson Establishment { get; set; } = new();
    public ICollection<ResponseAvailableEstablishmentProductJson> Products { get; set; } = [];
}

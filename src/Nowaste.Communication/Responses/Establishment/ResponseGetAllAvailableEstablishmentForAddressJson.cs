namespace Nowaste.Communication.Responses.Establishment;

public class ResponseGetAllAvailableEstablishmentForAddressJson
{
    public ResponseAvailableEstablishmentJson Establishment { get; set; } = new();
    public ICollection<ResponseAvailableEstablishmentProductJson> Products { get; set; } = [];
}

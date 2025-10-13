namespace Nowaste.Communication.Responses.Users;

public class ResponseGetPersonByIdJson {
    public string FullName { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
}

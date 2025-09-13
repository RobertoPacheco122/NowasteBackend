namespace Nowaste.Communication.Requests.Users;

public class RequestChangePasswordJson {
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

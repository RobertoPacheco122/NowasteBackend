using Nowaste.Domain.Entities;

namespace WebApi.Test.Resources;

public class UserIdentityManager(
    UserEntity user,
    string password,
    string token
    ) {
    public string GetEmail() => user.Email;

    public string GetFullName() => user.Person.FullName;

    public string GetPassword() => password;

    public string GetToken() => token;
}

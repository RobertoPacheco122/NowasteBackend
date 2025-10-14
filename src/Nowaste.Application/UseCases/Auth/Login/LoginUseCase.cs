using Nowaste.Communication.Requests.Auth;
using Nowaste.Communication.Responses.Users;
using Nowaste.Domain.Repositories.User;
using Nowaste.Domain.Security.Cryptography;
using Nowaste.Domain.Security.Tokens;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Auth.Login;

public class LoginUseCase(
    IUserReadOnlyRepository userReadOnlyRepository,
    IPasswordEncrypter passwordEncripter,
    IAccessTokenGenerator accessTokenGenerator
) : ILoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncripter = passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user =
            await _userReadOnlyRepository.GetUserByEmail(request.Email)
            ?? throw new InvalidLoginException("Email ou senha inválidos.");

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.PasswordHash);

        if (!passwordMatch)
            throw new InvalidLoginException("Email ou senha inválidos.");

        return new ResponseRegisteredUserJson
        {
            Name = user.Person.FullName.Split(" ").First(),
            Token = _accessTokenGenerator.Generate(user),
        };
    }
}

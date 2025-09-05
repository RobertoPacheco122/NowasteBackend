using Nowaste.Communication.Requests.Auth;
using Nowaste.Communication.Responses.Users;
using Nowaste.Domain.Repositories.Users;
using Nowaste.Domain.Security.Cryptography;
using Nowaste.Domain.Security.Tokens;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Auth.Login;

public class LoginUseCase(
    IUserRepository usersRepository,
    IPasswordEncrypter passwordEncripter,
    IAccessTokenGenerator accessTokenGenerator
    ) : ILoginUseCase {
    private readonly IUserRepository _usersRepository = usersRepository;
    private readonly IPasswordEncrypter _passwordEncripter = passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator = accessTokenGenerator;

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request) {
        var user = await _usersRepository.GetUserByEmail(request.Email) ??
            throw new InvalidLoginException("Email ou senha inválidos.");

        var passwordMatch = _passwordEncripter.Verify(request.Password, user.PasswordHash);

        if (!passwordMatch)
            throw new InvalidLoginException("Email ou senha inválidos.");

        return new ResponseRegisteredUserJson {
            Name = user.Person.FullName.Split(" ").First(),
            Token = _accessTokenGenerator.Generate(user)
        };
    }
}

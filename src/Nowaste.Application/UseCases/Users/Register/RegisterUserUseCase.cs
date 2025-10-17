using AutoMapper;
using Nowaste.Communication.Requests.Users;
using Nowaste.Communication.Responses.Users;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Enums;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Person;
using Nowaste.Domain.Repositories.User;
using Nowaste.Domain.Security.Cryptography;
using Nowaste.Domain.Security.Tokens;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Users.Register;

public class RegisterUserUseCase(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IUserWriteOnlyRepository userWriteOnlyRepository,
    IUserReadOnlyRepository userReadOnlyRepository,
    IPersonWriteOnlyRepository personWriteOnlyRepository,
    IPersonReadOnlyRepository personReadOnlyRepository,
    IPasswordEncrypter passwordEncripter,
    IAccessTokenGenerator tokenGenerator
) : IRegisterUserUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository = userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IPersonWriteOnlyRepository _personWriteOnlyRepository =
        personWriteOnlyRepository;
    private readonly IPersonReadOnlyRepository _personReadOnlyRepository = personReadOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncripter = passwordEncripter;
    private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var userEntity = _mapper.Map<UserEntity>(request);
        userEntity.CreatedAt = DateTime.UtcNow;
        userEntity.PasswordHash = _passwordEncripter.Encrypt(request.Password);
        userEntity.UserStatus = EUserStatus.Active;

        await _userWriteOnlyRepository.Add(userEntity);

        var personEntity = _mapper.Map<PersonEntity>(request);
        personEntity.CreatedAt = DateTime.UtcNow;
        personEntity.UserId = userEntity.Id;

        await _personWriteOnlyRepository.Add(personEntity);

        var token = _tokenGenerator.Generate(userEntity);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = request.FullName.Split(" ").First(),
            Token = token,
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
            throw new ErrorOnValidationException(
                ["Já existe um usuário cadastrado com este email."]
            );

        var phoneNumberExist = await _personReadOnlyRepository.ExistActiveUserWithPhoneNumber(
            request.PhoneNumber
        );

        if (phoneNumberExist)
            throw new ErrorOnValidationException(
                ["Já existe um usuário cadastrado com este número de celular."]
            );

        var cpfExist = await _userReadOnlyRepository.ExistActiveUserWithCpf(request.Cpf);

        if (cpfExist)
            throw new ErrorOnValidationException(["Já existe um usuário cadastrado com este CPF."]);

        if (!result.IsValid)
        {
            var errorsMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}

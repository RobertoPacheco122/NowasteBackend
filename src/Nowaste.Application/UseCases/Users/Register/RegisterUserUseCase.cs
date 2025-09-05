using AutoMapper;
using Nowaste.Application.UseCases.Persons.Register;
using Nowaste.Communication.Requests.Persons;
using Nowaste.Communication.Requests.Users;
using Nowaste.Communication.Responses.Users;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Enums;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Persons;
using Nowaste.Domain.Repositories.Users;
using Nowaste.Domain.Security.Cryptography;
using Nowaste.Domain.Security.Tokens;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Users.Register;

public class RegisterUserUseCase (
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRepository usersRepository,
        IPersonRepository personRepository,
        IPasswordEncrypter passwordEncripter,
        IAccessTokenGenerator tokenGenerator
    ) : IRegisterUserUseCase {
    private readonly IUserRepository _usersRepository = usersRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordEncrypter _passwordEncripter = passwordEncripter;
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request) {
        await Validate(request);

        var userEntity = _mapper.Map<UserEntity>(request);
        userEntity.PasswordHash = _passwordEncripter.Encrypt(request.Password);
        userEntity.UserStatus = EUserStatus.PendingVerification;
        userEntity.CreatedAt = DateTime.UtcNow;

        await _usersRepository.Add(userEntity);

        var personEntity = _mapper.Map<PersonEntity>(request);
        personEntity.UserId = userEntity.Id;
        personEntity.CreatedAt = DateTime.UtcNow;

        await _personRepository.Add(personEntity);

        var token = _tokenGenerator.Generate(userEntity);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson {
            Name = request.FullName.Split(" ").First(),
            Token = token,
        };
    }

    private async Task Validate(RequestRegisterUserJson request) {
        var result = new RegisterUserValidator().Validate(request);

        var emailExist = await _usersRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(
                string.Empty,
                "Já existe um usuário cadastrado com este email.")
            );
        

        if (!result.IsValid) {
            var errorsMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessages);
        }
    }
}

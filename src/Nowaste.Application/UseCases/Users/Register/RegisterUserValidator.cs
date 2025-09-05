using FluentValidation;
using Nowaste.Application.UseCases.Persons;
using Nowaste.Communication.Requests.Users;
using Nowaste.Domain.Enums;

namespace Nowaste.Application.UseCases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson> {
    private static readonly HashSet<string> ValidRoles = [
        ..typeof(Roles)
        .GetFields()
        .Where(f => f.IsLiteral && !f.IsInitOnly)
        .Select(f => f.GetRawConstantValue()?.ToString()!)
    ];

    public RegisterUserValidator() {
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("O email é obrigatório.")
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) is false, ApplyConditionTo.CurrentValidator)
            .WithMessage("O email não é válido.");

        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator<RequestRegisterUserJson>());

        RuleFor(user => user)
            .Must(user => (user.InstitutionId.HasValue && user.EstablishmentId.HasValue) is false)
            .WithMessage("O usuário não pode estar associado a um estabelecimento e instituição ao mesmo tempo.");

        RuleFor(user => user.Role)
                .NotEmpty()
                .WithMessage("A role é obrigatória.");

        RuleFor(user => user.Role)
            .Must(role => ValidRoles.Contains(role))
            .When(user => !string.IsNullOrWhiteSpace(user.Role), ApplyConditionTo.CurrentValidator)
            .WithMessage("A role informada não é válida.");

        When(user => (user.InstitutionId.HasValue && user.EstablishmentId.HasValue) is false, () =>
        {
            RuleFor(user => user.Role)
                .Must(role => role.StartsWith("institution"))
                .When(user =>
                    !string.IsNullOrWhiteSpace(user.Role) &&
                    user.InstitutionId.HasValue &&
                    user.InstitutionId != Guid.Empty,
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("A role deve ser do tipo 'institution' quando InstitutionId estiver preenchido.");

            RuleFor(user => user.Role)
                .Must(role => role.StartsWith("establishment"))
                .When(user =>
                    !string.IsNullOrWhiteSpace(user.Role) &&
                    user.EstablishmentId.HasValue &&
                    user.EstablishmentId != Guid.Empty,
                    ApplyConditionTo.CurrentValidator
                )
                .WithMessage("A role deve ser do tipo 'establishment' quando EstablishmentId estiver preenchido.");
        });

        RuleFor(user => user.FullName)
            .NotEmpty()
            .WithMessage("O nome completo é obrigatório.");

        RuleFor(user => user.Cpf)
            .SetValidator(new CpfValidator<RequestRegisterUserJson>());

        RuleFor(user => user.PhoneNumber)
            .NotEmpty()
            .WithMessage("O celular é obrigatório")

            .MinimumLength(10)
            .When(user => string.IsNullOrWhiteSpace(user.PhoneNumber) is false, ApplyConditionTo.CurrentValidator)
            .WithMessage("O celular deve ter no mínimo 10 digitos e no máximo 11 digitos.")

            .MaximumLength(11)
            .When(user => string.IsNullOrWhiteSpace(user.PhoneNumber) is false, ApplyConditionTo.CurrentValidator)
            .WithMessage("O celular deve ter no mínimo 10 digitos e no máximo 11 digitos.");

        RuleFor(user => user.BirthDate)
            .NotEmpty()
            .WithMessage("A data de nascimento é obrigatória.")
            .Must(HaveAtLeast18Years)
            .WithMessage("O usuário deve ter pelo menos 18 anos.");
    }

    private bool HaveAtLeast18Years(DateOnly birthDate) {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
            age--;

        return age >= 18;
    }
}

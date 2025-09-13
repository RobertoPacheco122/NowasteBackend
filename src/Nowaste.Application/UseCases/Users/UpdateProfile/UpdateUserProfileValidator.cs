using FluentValidation;
using Nowaste.Communication.Requests.Users;
using Nowaste.Domain.Enums;

namespace Nowaste.Application.UseCases.Users.UpdateProfile;

public class UpdateUserProfileValidator : AbstractValidator<RequestUpdateUserProfileJson> {
    private static readonly HashSet<string> ValidRoles = [
        ..typeof(Roles)
        .GetFields()
        .Where(f => f.IsLiteral && !f.IsInitOnly)
        .Select(f => f.GetRawConstantValue()?.ToString()!)
    ];

    public UpdateUserProfileValidator() {
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

        When(user => (user.InstitutionId.HasValue && user.EstablishmentId.HasValue) is false, () => {
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

        RuleFor(user => user.UserStatus)
                .NotEmpty()
                .WithMessage("O status do usuário é obrigatório.")
                .IsInEnum()
                .WithMessage("O status do usuário é inválido.");

        RuleFor(user => user.PhoneNumber)
            .NotEmpty()
            .WithMessage("O celular é obrigatório.")

            .MinimumLength(10)
            .When(user => string.IsNullOrWhiteSpace(user.PhoneNumber) is false, ApplyConditionTo.CurrentValidator)
            .WithMessage("O celular deve ter no mínimo 10 digitos e no máximo 11 digitos.")

            .MaximumLength(11)
            .When(user => string.IsNullOrWhiteSpace(user.PhoneNumber) is false, ApplyConditionTo.CurrentValidator)
            .WithMessage("O celular deve ter no mínimo 10 digitos e no máximo 11 digitos.");
    }
}

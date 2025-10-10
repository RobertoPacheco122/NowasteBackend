using FluentValidation;
using Nowaste.Communication.Requests.Establishment;
using Nowaste.Domain.Enums;

namespace Nowaste.Application.UseCases.Establishment.VinculateEmployee;

public class VinculateEmployeeToEstablishmentValidator : AbstractValidator<RequestVinculateEmployeeToEstablishmentJson> {
    private static readonly HashSet<string> ValidRoles = [
        ..typeof(Roles)
        .GetFields()
        .Where(f => f.IsLiteral && !f.IsInitOnly)
        .Select(f => f.GetRawConstantValue()?.ToString()!)
    ];

    public VinculateEmployeeToEstablishmentValidator() {
        RuleFor(vinculate => vinculate.EstablishmentId)
            .NotEmpty()
            .WithMessage("A propriedade 'establishmentId' é obrigatória.");

        RuleFor(vinculate => vinculate.UserId)
            .NotEmpty()
            .WithMessage("A propriedade 'userId' é obrigatória.");

        RuleFor(vinculate => vinculate.Role)
            .Must(role => ValidRoles.Contains(role))
            .When(vinculate => !string.IsNullOrWhiteSpace(vinculate.Role), ApplyConditionTo.CurrentValidator)
            .WithMessage("A role informada não é válida.")
            .When(vinculate => vinculate.Role.StartsWith("establishment"), ApplyConditionTo.CurrentValidator)
            .WithMessage("A role deve ser do tipo 'establishment'.");
    }
}

using FluentValidation;
using Nowaste.Communication.Requests.Establishment;

namespace Nowaste.Application.UseCases.Establishment.Update;

public class UpdateEstablishmentValidator : AbstractValidator<RequestUpdateEstablishmentJson>
{
    public UpdateEstablishmentValidator()
    {
        RuleFor(establishment => establishment.ExhibitionName)
            .NotEmpty()
            .WithMessage("O nome de exibição é obrigatório");

        RuleFor(establishment => establishment.Email)
            .NotEmpty()
            .WithMessage("O email é obrigatório.")
            .EmailAddress()
            .When(
                establishment => string.IsNullOrWhiteSpace(establishment.Email) is false,
                ApplyConditionTo.CurrentValidator
            )
            .WithMessage("O email não é válido.");

        RuleFor(establishment => establishment.Status)
            .NotEmpty()
            .WithMessage("O status do estabelecimento é obrigatório.")
            .IsInEnum()
            .WithMessage("O status do estabelecimento é inválido.");
    }
}

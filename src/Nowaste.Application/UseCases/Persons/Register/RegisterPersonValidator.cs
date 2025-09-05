using FluentValidation;
using Nowaste.Communication.Requests.Persons;

namespace Nowaste.Application.UseCases.Persons.Register;

public class RegisterPersonValidator : AbstractValidator<RequestRegisterPersonJson> {
    public RegisterPersonValidator() {
        RuleFor(person => person.FullName)
            .NotEmpty()
            .WithMessage("O nome completo é obrigatório");

        RuleFor(person => person.Cpf)
            .SetValidator(new CpfValidator<RequestRegisterPersonJson>());

        RuleFor(person => person.PhoneNumber)
            .NotEmpty()
            .WithMessage("O celular é obrigatório")
            .MinimumLength(10)
            .WithMessage("O celular deve ter no mínimo 10 digitos e no máximo 11 digitos.")
            .MaximumLength(11)
            .WithMessage("O celular deve ter no mínimo 10 digitos e no máximo 11 digitos.");

        RuleFor(person => person.BirthDate)
            .NotEmpty()
            .WithMessage("A data de nascimento é obrigatória.")
            .LessThan(DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("A data de nascimento deve ser anterior à data de hoje.")
            .Must(HaveAtLeast18Years)
            .WithMessage("O usuário deve ter pelo menos 18 anos.");

        RuleFor(person => person.UserId)
            .NotEmpty()
            .WithMessage("O userId é obrigatório.");

        When(person => person.InstitutionId.HasValue && person.EstablishmentId.HasValue, () => {
            RuleFor(person => person)
                .Must(person => false)
                .WithMessage("O usuário não pode estar associado a um estabelecimento e uma instituição");
        });
    }

    private bool HaveAtLeast18Years(DateOnly birthDate) {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
            age--;

        return age >= 18;
    }
}

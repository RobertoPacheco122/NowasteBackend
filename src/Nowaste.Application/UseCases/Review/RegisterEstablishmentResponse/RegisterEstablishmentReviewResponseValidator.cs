using FluentValidation;
using Nowaste.Communication.Requests.Review;

namespace Nowaste.Application.UseCases.Review.RegisterEstablishmentResponse;

public class RegisterEstablishmentReviewResponseValidator
    : AbstractValidator<RequestRegisterEstablishmentReviewResponseJson>
{
    public RegisterEstablishmentReviewResponseValidator()
    {
        RuleFor(review => review.Response)
            .NotEmpty()
            .WithMessage("A propriedade 'response' não pode ser vazia.")
            .MaximumLength(1000)
            .WithMessage("A propriedade 'response' não pode exceder 1000 caracteres.");
    }
}

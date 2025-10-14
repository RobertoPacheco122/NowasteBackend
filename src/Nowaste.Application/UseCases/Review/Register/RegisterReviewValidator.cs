using FluentValidation;
using Nowaste.Communication.Requests.Review;

namespace Nowaste.Application.UseCases.Review.Register;

public class RegisterReviewValidator : AbstractValidator<RequestRegisterReviewJson>
{
    public RegisterReviewValidator()
    {
        RuleFor(review => review.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage("A propriedade 'rating' deve ter valores entre 1 a 5.");

        RuleFor(review => review.PersonComment)
            .MaximumLength(140)
            .WithMessage("A propriedade 'personComment' deve ter no máximo 140 caracteres.");

        RuleFor(review => review.OrderId)
            .NotEmpty()
            .WithMessage("A propriedade 'orderId' é obrigatória.")
            .NotEqual(Guid.Empty)
            .WithMessage("A propriedade 'orderId' deve ser um UUID válido.");
    }
}

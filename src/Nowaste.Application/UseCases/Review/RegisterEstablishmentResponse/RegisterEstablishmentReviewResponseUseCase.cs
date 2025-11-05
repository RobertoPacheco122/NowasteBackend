using Nowaste.Communication.Requests.Review;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Review;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Review.RegisterEstablishmentResponse;

public class RegisterEstablishmentReviewResponseUseCase(
    IUnitOfWork unitOfWork,
    ILoggedUser loggedUser,
    IReviewUpdateOnlyRepository reviewUpdateOnlyRepository
) : IRegisterEstablishmentReviewResponseUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IReviewUpdateOnlyRepository _reviewUpdateOnlyRepository =
        reviewUpdateOnlyRepository;

    public async Task Execute(Guid reviewId, RequestRegisterEstablishmentReviewResponseJson request)
    {
        Validate(request);

        var loggedUserEntity =
            await loggedUser.Get() ?? throw new NotFoundException("Usuário não foi encontrado.");

        var reviewEntity =
            await _reviewUpdateOnlyRepository.GetById(reviewId)
            ?? throw new NotFoundException("A avaliação não foi encontrada.");

        if (reviewEntity.Order.EstablishmentId != loggedUserEntity.Person.EstablishmentId)
            throw new ForbiddenException(
                "O usuário não possui permissão para responder essa avaliação."
            );

        reviewEntity.EstablishmentResponse = request.Response;
        reviewEntity.UpdatedAt = DateTime.UtcNow;

        _reviewUpdateOnlyRepository.Update(reviewEntity);

        await _unitOfWork.Commit();
    }

    public static void Validate(RequestRegisterEstablishmentReviewResponseJson request)
    {
        var validationResult = new RegisterEstablishmentReviewResponseValidator().Validate(request);

        if (validationResult.IsValid is false)
        {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}

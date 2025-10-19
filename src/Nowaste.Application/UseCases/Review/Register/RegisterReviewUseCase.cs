using AutoMapper;
using Nowaste.Communication.Requests.Review;
using Nowaste.Communication.Responses.Review;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Enums;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Order;
using Nowaste.Domain.Repositories.Review;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Review.Register;

public class RegisterReviewUseCase(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser,
    IReviewWriteOnlyRepository reviewWriteOnlyRepository,
    IReviewReadOnlyRepository reviewReadOnlyRepository,
    IOrderReadOnlyRepository orderReadOnlyRepository
) : IRegisterReviewUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IReviewWriteOnlyRepository _reviewWriteOnlyRepository =
        reviewWriteOnlyRepository;
    private readonly IReviewReadOnlyRepository _reviewReadOnlyRepository = reviewReadOnlyRepository;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository = orderReadOnlyRepository;

    public async Task<ResponseRegisteredReviewJson> Execute(RequestRegisterReviewJson request)
    {
        var loggedUserEntity =
            await _loggedUser.Get() ?? throw new NotFoundException("Usuário não encontrado.");

        var orderEntity =
            await _orderReadOnlyRepository.GetById(request.OrderId)
            ?? throw new NotFoundException("Pedido a ser avaliado não encontrado.");

        await Validate(request, loggedUserEntity, orderEntity);

        var reviewEntity = _mapper.Map<ReviewEntity>(request);
        reviewEntity.CreatedAt = DateTime.UtcNow;
        reviewEntity.PersonId = loggedUserEntity.Person.Id;
        reviewEntity.EstablishmentId = orderEntity.EstablishmentId;
        reviewEntity.ReviewDate = DateTime.UtcNow;

        await _reviewWriteOnlyRepository.Add(reviewEntity);

        await _unitOfWork.Commit();

        return new ResponseRegisteredReviewJson
        {
            Id = reviewEntity.Id,
            Rating = reviewEntity.Rating,
        };
    }

    public async Task Validate(
        RequestRegisterReviewJson request,
        UserEntity loggedUser,
        OrderEntity order
    )
    {
        var validationResult = new RegisterReviewValidator().Validate(request);

        if (order.PersonId != loggedUser.Person.Id)
            throw new ForbiddenException("O pedido a ser avaliado não pertence ao usuário.");

        if (order.OrderStatus is not EOrderStatus.Delivered)
            validationResult.Errors.Add(
                new FluentValidation.Results.ValidationFailure(
                    string.Empty,
                    "Não é possível avaliar um pedido que ainda não foi entregue."
                )
            );

        var existReviewForGivenOrder = await _reviewReadOnlyRepository.ExistActiveForOrderId(
            request.OrderId
        );

        if (existReviewForGivenOrder)
            validationResult.Errors.Add(
                new FluentValidation.Results.ValidationFailure(
                    string.Empty,
                    "Já existe uma avaliação para o pedido informado."
                )
            );

        if (validationResult.IsValid is false)
        {
            var errorMessages = validationResult
                .Errors.Select(error => error.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}

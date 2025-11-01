using AutoMapper;
using Nowaste.Communication.Responses.Review;
using Nowaste.Domain.Repositories.Review;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Establishment.GetAllReviews;

public class GetAllEstablishmentReviewsUseCase(
    IMapper mapper,
    IReviewReadOnlyRepository reviewReadOnlyRepository
) : IGetAllEstablishmentReviewsUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IReviewReadOnlyRepository _reviewReadOnlyRepository = reviewReadOnlyRepository;

    public async Task<ICollection<ResponseGetReviewByIdJson>> Execute(Guid establishmentId)
    {
        var reviewsEntities = await _reviewReadOnlyRepository.GetAllByEstablishment(
            establishmentId
        );

        if (reviewsEntities.Count == 0)
            throw new NotFoundException("Nenhuma avaliação encontrada para este estabelecimento.");

        return _mapper.Map<ICollection<ResponseGetReviewByIdJson>>(reviewsEntities);
    }
}

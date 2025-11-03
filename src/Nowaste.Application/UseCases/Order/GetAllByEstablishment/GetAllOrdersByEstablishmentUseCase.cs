using AutoMapper;
using Nowaste.Communication.Responses.Order;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Order;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Order.GetAllByEstablishment;

public class GetAllOrdersByEstablishmentUseCase(
    IMapper mapper,
    ILoggedUser loggedUser,
    IOrderReadOnlyRepository orderReadOnlyRepository
) : IGetAllOrdersByEstablishmentUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository = orderReadOnlyRepository;

    public async Task<ICollection<ResponseGetAllOrdersByEstablishmentJson>> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        Validate(loggedUser);

        var ordersEntities = await _orderReadOnlyRepository.GetAllByEstablishmentId(
            loggedUser.Person.EstablishmentId!.Value
        );

        if (ordersEntities.Count == 0)
            throw new NotFoundException("Nenhum pedido encontrado para este estabelecimento.");

        return _mapper.Map<ICollection<ResponseGetAllOrdersByEstablishmentJson>>(ordersEntities);
    }

    public static void Validate(UserEntity? user)
    {
        if (user is null)
            throw new NotFoundException("Usuário não encontrado.");

        if (user.Person.EstablishmentId is null)
            throw new ForbiddenException("Estabelecimento não encontrado para o usuário logado.");
    }
}

using Nowaste.Communication.Enums;
using Nowaste.Communication.Requests.Order;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Enums;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Order;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Order.UpdateStatus;

public class UpdateOrderStatusUseCase(
    IUnitOfWork unitOfWork,
    ILoggedUser loggedUser,
    IOrderUpdateOnlyRepository orderUpdateOnlyRepository
) : IUpdateOrderStatusUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IOrderUpdateOnlyRepository _orderUpdateOnlyRepository =
        orderUpdateOnlyRepository;

    public async Task Execute(Guid orderId, RequestUpdateOrderStatusJson request)
    {
        var orderEntity = await _orderUpdateOnlyRepository.GetById(orderId);
        var loggedUser = await _loggedUser.Get();

        Validate(orderEntity, loggedUser);

        orderEntity!.OrderStatus = (Domain.Enums.EOrderStatus)request.Status;

        _orderUpdateOnlyRepository.Update(orderEntity);

        await _unitOfWork.Commit();
    }

    public static void Validate(OrderEntity? order, UserEntity? user)
    {
        if (order is null)
            throw new NotFoundException("Pedido não encontrado.");

        if (user is null)
            throw new NotFoundException("Usuário não encontrado.");

        if (user.Person.EstablishmentId != order.EstablishmentId)
            throw new ForbiddenException("O usuário logado não pertence ao estabelecimento.");
    }
}

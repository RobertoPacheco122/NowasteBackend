using Nowaste.Communication.Responses.Order;

namespace Nowaste.Application.UseCases.Order.GetAllByEstablishment;

public interface IGetAllOrdersByEstablishmentUseCase
{
    Task<ICollection<ResponseGetAllOrdersByEstablishmentJson>> Execute();
}

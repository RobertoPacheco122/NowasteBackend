using Nowaste.Communication.Responses.Order;

namespace Nowaste.Application.UseCases.Order.GetAllByPerson;

public interface IGetAllOrdersByPersonUseCase
{
    Task<ICollection<ResponseGetOrderByIdJson>> Execute();
}

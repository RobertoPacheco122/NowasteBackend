using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Order;

public interface IOrderReadOnlyRepository
{
    Task<ICollection<OrderEntity>> GetAllByPerson(Guid id);
    Task<OrderEntity?> GetById(Guid id);
    Task<OrderEntity?> GetByPaymentSessionId(string id);
    Task<OrderItemEntity?> GetOrderItemById(Guid id);
    Task<int> GetOrdersCountByEstablishment(Guid establishmentId);
}

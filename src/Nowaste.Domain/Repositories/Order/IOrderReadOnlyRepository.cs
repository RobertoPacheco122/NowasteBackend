using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Order;

public interface IOrderReadOnlyRepository
{
    Task<OrderEntity?> GetById(Guid id);
    Task<OrderItemEntity?> GetOrderItemById(Guid id);
    Task<int> GetOrdersCountByEstablishment(Guid establishmentId);
}

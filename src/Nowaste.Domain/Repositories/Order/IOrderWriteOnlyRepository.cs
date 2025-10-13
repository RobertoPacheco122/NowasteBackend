using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Order;

public interface IOrderWriteOnlyRepository
{
    Task Add(OrderEntity order);
    Task AddOrderItem(OrderItemEntity orderItem);
    Task AddManyOrderItems(ICollection<OrderItemEntity> orders);
}

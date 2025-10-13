using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Order;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Order;

internal class OrderWriteOnlyRepository(NowasteDbContext dbContext) : IOrderWriteOnlyRepository {
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task Add(OrderEntity order) {
        await _dbContext.Orders.AddAsync(order);
    }

    public async Task AddManyOrderItems(ICollection<OrderItemEntity> orders) {
        await _dbContext.OrderItems.AddRangeAsync(orders);
    }

    public async Task AddOrderItem(OrderItemEntity orderItem) {
        await _dbContext.OrderItems.AddAsync(orderItem);
    }
}

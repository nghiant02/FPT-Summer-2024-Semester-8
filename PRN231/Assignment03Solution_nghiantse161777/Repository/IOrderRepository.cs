using BussiniseObject.Models;

namespace Repository;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(Order order);
    Task<Order> UpdateOrderAsync(int id,Order order);
    Task<Order> DeleteOrderAsync(int id);
}
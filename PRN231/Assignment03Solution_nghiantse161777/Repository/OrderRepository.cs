using BussiniseObject.Models;
using Dao;

namespace Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDao _orderDao;

    public OrderRepository(OrderDao orderDao)
    {
        _orderDao = orderDao;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return await _orderDao.GetOrders();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _orderDao.GetOrder(id);
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        return await _orderDao.CreateOrder(order);
    }

    public async Task<Order> UpdateOrderAsync(int id,Order order)
    {
        return await _orderDao.UpdateOrder(id, order);
    }

    public async Task<Order> DeleteOrderAsync(int id)
    {
        return await _orderDao.DeleteOrder(id);
    }
}
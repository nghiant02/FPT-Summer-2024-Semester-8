using BussiniseObject.Models;
using Microsoft.EntityFrameworkCore;

namespace Dao;

public class OrderDao
{
    private readonly ApplicationDbContext _context = new();
    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }
    public async Task<Order> GetOrder(int id)
    {
        return await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
    }
    public async Task<Order> CreateOrder(Order order)
    {
        var lastOrder = await _context.Orders.OrderByDescending(x => x.OrderId).FirstOrDefaultAsync();
        order.OrderId = lastOrder == null ? 1 : lastOrder.OrderId + 1;
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }
    public async Task<Order> UpdateOrder(int id, Order order)
    {
        var orderToUpdate = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        if (orderToUpdate == null) return null;
        orderToUpdate.OrderDate = order.OrderDate;
        orderToUpdate.RequiredDate = order.RequiredDate;
        orderToUpdate.ShippedDate = order.ShippedDate;
        orderToUpdate.Freight = order.Freight;
        await _context.SaveChangesAsync();
        return orderToUpdate;
    }
    public async Task<Order> DeleteOrder(int id)
    {
        var orderToDelete = await _context.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        if (orderToDelete == null) return null;
        _context.Orders.Remove(orderToDelete);
        await _context.SaveChangesAsync();
        return orderToDelete;
    }
    
}
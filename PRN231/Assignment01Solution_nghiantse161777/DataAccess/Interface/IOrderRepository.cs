using BusinessObject.DTO;
using BusinessObject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDTO>> GetOrdersAsync();
        Task<OrderDTO> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(OrderCreateUpdateDTO orderCreateUpdateDTO);
        Task UpdateOrderAsync(int orderId, OrderCreateUpdateDTO orderCreateUpdateDTO);
        Task DeleteOrderAsync(int orderId);
    }
}

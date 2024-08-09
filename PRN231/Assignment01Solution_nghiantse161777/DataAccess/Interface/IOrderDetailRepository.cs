using BusinessObject.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetailDTO>> GetOrderDetailsAsync();
        Task<OrderDetailDTO> GetOrderDetailByIdAsync(int orderId, int productId);
        Task AddOrderDetailAsync(OrderDetailCreateUpdateDTO orderDetailCreateUpdateDTO);
        Task UpdateOrderDetailAsync(int orderId, int productId, OrderDetailCreateUpdateDTO orderDetailCreateUpdateDTO);
        Task DeleteOrderDetailAsync(int orderId, int productId);
    }
}

using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly eStoreDBContext _context;

        public OrderDetailRepository(eStoreDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetailDTO>> GetOrderDetailsAsync()
        {
            var orderDetails = await _context.OrderDetails
                .Include(od => od.Product)
                .ToListAsync();

            var orderDetailDTOs = orderDetails.Select(od => new OrderDetailDTO
            {
                OrderId = od.OrderId,
                ProductId = od.ProductId,
                ProductName = od.Product.ProductName,
                UnitPrice = od.UnitPrice,
                Quantity = od.Quantity,
                Discount = (float)od.Discount
            });

            return orderDetailDTOs;
        }

        public async Task<OrderDetailDTO> GetOrderDetailByIdAsync(int orderId, int productId)
        {
            var orderDetail = await _context.OrderDetails
                .Include(od => od.Product)
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);

            if (orderDetail == null) return null;

            var orderDetailDTO = new OrderDetailDTO
            {
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                ProductName = orderDetail.Product.ProductName,
                UnitPrice = orderDetail.UnitPrice,
                Quantity = orderDetail.Quantity,
                Discount = (float)orderDetail.Discount
            };

            return orderDetailDTO;
        }

        public async Task AddOrderDetailAsync(OrderDetailCreateUpdateDTO orderDetailCreateUpdateDTO)
        {
            var orderDetail = new OrderDetail
            {
                OrderId = orderDetailCreateUpdateDTO.OrderId,
                ProductId = orderDetailCreateUpdateDTO.ProductId,
                UnitPrice = orderDetailCreateUpdateDTO.UnitPrice,
                Quantity = orderDetailCreateUpdateDTO.Quantity,
                Discount = orderDetailCreateUpdateDTO.Discount
            };

            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetailAsync(int orderId, int productId, OrderDetailCreateUpdateDTO orderDetailCreateUpdateDTO)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(orderId, productId);
            if (orderDetail != null)
            {
                orderDetail.UnitPrice = orderDetailCreateUpdateDTO.UnitPrice;
                orderDetail.Quantity = orderDetailCreateUpdateDTO.Quantity;
                orderDetail.Discount = orderDetailCreateUpdateDTO.Discount;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderDetailAsync(int orderId, int productId)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(orderId, productId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly eStoreDBContext _context;

        public OrderRepository(eStoreDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ToListAsync();

            var orderDTOs = orders.Select(order => new OrderDTO
            {
                OrderId = order.OrderId,
                MemberId = order.MemberId,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                Freight = order.Freight,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    ProductName = od.Product.ProductName,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    Discount = (float)od.Discount
                }).ToList()
            });

            return orderDTOs;
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null) return null;

            var orderDTO = new OrderDTO
            {
                OrderId = order.OrderId,
                MemberId = order.MemberId,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                Freight = order.Freight,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDTO
                {
                    OrderId = od.OrderId,
                    ProductId = od.ProductId,
                    ProductName = od.Product.ProductName,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    Discount = (float)od.Discount
                }).ToList()
            };

            return orderDTO;
        }

        public async Task AddOrderAsync(OrderCreateUpdateDTO orderCreateUpdateDTO)
        {
            var order = new Order
            {
                MemberId = orderCreateUpdateDTO.MemberId,
                OrderDate = orderCreateUpdateDTO.OrderDate,
                RequiredDate = orderCreateUpdateDTO.RequiredDate,
                ShippedDate = orderCreateUpdateDTO.ShippedDate,
                Freight = orderCreateUpdateDTO.Freight
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(int orderId, OrderCreateUpdateDTO orderCreateUpdateDTO)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.MemberId = orderCreateUpdateDTO.MemberId;
                order.OrderDate = orderCreateUpdateDTO.OrderDate;
                order.RequiredDate = orderCreateUpdateDTO.RequiredDate;
                order.ShippedDate = orderCreateUpdateDTO.ShippedDate;
                order.Freight = orderCreateUpdateDTO.Freight;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}

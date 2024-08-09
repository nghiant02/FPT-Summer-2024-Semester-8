using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private readonly eStoreDBContext _context;
        private readonly IMapper _mapper;

        public OrderDAO(eStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersAsync() =>
            _mapper.Map<IEnumerable<OrderDTO>>(await _context.Orders.ToListAsync());

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId) =>
            _mapper.Map<OrderDTO>(await _context.Orders.FindAsync(orderId));

        public async Task AddOrderAsync(OrderCreateUpdateDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(int orderId, OrderCreateUpdateDTO orderDTO)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _mapper.Map(orderDTO, order);
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

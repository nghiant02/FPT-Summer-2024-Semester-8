using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private readonly eStoreDBContext _context;
        private readonly IMapper _mapper;

        public OrderDetailDAO(eStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDetailDTO>> GetOrderDetailsAsync() =>
            _mapper.Map<IEnumerable<OrderDetailDTO>>(await _context.OrderDetails.ToListAsync());

        public async Task<OrderDetailDTO> GetOrderDetailByIdAsync(int orderId, int productId) =>
            _mapper.Map<OrderDetailDTO>(await _context.OrderDetails.FindAsync(orderId, productId));

        public async Task AddOrderDetailAsync(OrderDetailCreateUpdateDTO orderDetailDTO)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDTO);
            await _context.OrderDetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetailAsync(int orderId, int productId, OrderDetailCreateUpdateDTO orderDetailDTO)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(orderId, productId);
            if (orderDetail != null)
            {
                _mapper.Map(orderDetailDTO, orderDetail);
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

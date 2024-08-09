using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailDTO>>> GetOrderDetails()
        {
            var orderDetails = await _orderDetailRepository.GetOrderDetailsAsync();
            return Ok(orderDetails);
        }

        [HttpGet("{orderId}/{productId}")]
        public async Task<ActionResult<OrderDetailDTO>> GetOrderDetail(int orderId, int productId)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderId, productId);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return Ok(orderDetail);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetailDTO>> CreateOrderDetail([FromBody] OrderDetailCreateUpdateDTO orderDetailCreateUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _orderDetailRepository.AddOrderDetailAsync(orderDetailCreateUpdateDTO);

            var orderDetailDTO = _mapper.Map<OrderDetailDTO>(orderDetailCreateUpdateDTO);

            return CreatedAtAction(nameof(GetOrderDetail), new { orderId = orderDetailDTO.OrderId, productId = orderDetailDTO.ProductId }, orderDetailDTO);
        }

        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> UpdateOrderDetail(int orderId, int productId, [FromBody] OrderDetailCreateUpdateDTO orderDetailCreateUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingOrderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderId, productId);

            if (existingOrderDetail == null)
            {
                return NotFound();
            }

            await _orderDetailRepository.UpdateOrderDetailAsync(orderId, productId, orderDetailCreateUpdateDTO);

            return NoContent();
        }

        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> DeleteOrderDetail(int orderId, int productId)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderId, productId);

            if (orderDetail == null)
            {
                return NotFound();
            }

            await _orderDetailRepository.DeleteOrderDetailAsync(orderId, productId);

            return NoContent();
        }
    }
}

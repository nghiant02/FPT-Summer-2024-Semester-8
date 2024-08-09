using AutoMapper;
using BussiniseObject.Dto;
using BussiniseObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controller;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderController(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    [HttpGet("GetOrders")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderRepository.GetOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("GetOrderById/{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpPost("CreateOrder")]
    public async Task<IActionResult> CreateOrder(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        var newOrder = await _orderRepository.CreateOrderAsync(order);
        var orderResponse = _mapper.Map<OrderDto>(newOrder);
        return Ok(orderResponse);
    }

    [HttpPut("UpdateOrder/{id}")]
    public async Task<IActionResult> UpdateOrder(int id, OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        var updatedOrder = await _orderRepository.UpdateOrderAsync(id, order);
        if (updatedOrder == null) return NotFound();
        return Ok(updatedOrder);
    }

    [HttpDelete("DeleteOrder/{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var deletedOrder = await _orderRepository.DeleteOrderAsync(id);
        if (deletedOrder == null) return NotFound();
        return Ok(deletedOrder);
    }
}
using EXE201.BLL.Interfaces;
using EXE201.BLL.Services;
using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace EXE201.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalOrderController : Controller
    {
        private readonly IRentalOrderServices _rentalOrderServices;
        private readonly IRentalOrderDetailServices _rentalOrderDetailServices;

        public RentalOrderController(IRentalOrderServices rentalOrderServices,
            IRentalOrderDetailServices rentalOrderDetailServices)
        {
            _rentalOrderServices = rentalOrderServices;
            _rentalOrderDetailServices = rentalOrderDetailServices;
        }

        [HttpGet("GetRentalOrderDetailByStaff")]
        public async Task<IActionResult> GetRentalOrderDetail(int pageNumber, int pageSize, OrderStatus? status = null)
        {
            var result = await _rentalOrderDetailServices.GetRentalOrderByStaff(pageNumber, pageSize, status);
            return Ok(result);
        }

        [HttpGet("GetRentalOrder")]
        public async Task<IActionResult> GetRentalOrders(int pageNumber, int pageSize)
        {
            var result = await _rentalOrderServices.GetRentalOrders(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("GetRentalOrdersByStatus")]
        public async Task<IActionResult> GetRentalOrdersByStatus(OrderStatus? status, int pageNumber, int pageSize)
        {
            var result = await _rentalOrderServices.GetRentalOrdersByStatus(status, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("GetReturnOrders")]
        public async Task<IActionResult> GetReturnOrders(int pageNumber, int pageSize)
        {
            var result = await _rentalOrderServices.GetReturnOrders(pageNumber, pageSize);
            return Ok(result);
        }


        //[HttpPost("CancelOrder")]
        //public async Task<IActionResult> CancelOrder([FromQuery] int orderId)
        //{
        //    var response = await _rentalOrderServices.CancelOrderAsync(orderId);
        //    if (response.Status == "Error")
        //    {
        //        return Conflict(response);
        //    }
        //    return Ok(response);
        //}

        //[HttpPost("ReturnOrder")]
        //public async Task<IActionResult> ReturnOrder([FromQuery] int orderId, string returnReason)
        //{
        //    var response = await _rentalOrderServices.ReturnOrderAsync(orderId, returnReason);
        //    if (response.Status == "Error")
        //    {
        //        return Conflict(response);
        //    }
        //    return Ok(response);
        //}

        //[HttpPost("ReturnItem")]
        //public async Task<IActionResult> ReturnItem([FromQuery] ReturnItemDTO returnItem)
        //{
        //    var response = await _rentalOrderServices.ReturnItem(returnItem);
        //    if (response.Status == "Error")
        //    {
        //        return Conflict(response);
        //    }
        //    return Ok(response);
        //}

        //[HttpGet("ViewOrderStatus/{orderId}")]
        //public async Task<IActionResult> ViewOrderStatus(int orderId)
        //{
        //    var orderStatus = await _rentalOrderServices.GetOrderStatus(orderId);
        //    if (orderStatus == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(orderStatus);
        //}

        //[HttpGet("GetRentalOrderByUserId")]
        //public async Task<IActionResult> GetRentalOrderByUserId([FromQuery] int userId)
        //{
        //    var rentalOrders = await _rentalOrderServices.GetRentalOrderByUserId(userId);

        //    if (rentalOrders == null)
        //    {
        //        return NotFound(new { Message = "No rental orders found for the user." });
        //    }

        //    return Ok(rentalOrders);
        //}

        //[HttpGet("GetRentalOrdersByUserId")]
        //public async Task<IActionResult> GetRentalOrdersByUserId([FromQuery] int userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        //{
        //    var rentalOrders = await _rentalOrderServices.GetRentalOrdersByUserId(userId, pageNumber, pageSize);

        //    if (rentalOrders == null)
        //    {
        //        return NotFound(new { Message = "No rental orders found for the user." });
        //    }

        //    return Ok(rentalOrders);
        //}

        [HttpGet("GetPagedRentalOrderDetailsByUserId")]
        public async Task<IActionResult> GetPagedRentalOrderDetailsByUserId([FromQuery] int userId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var rentalOrderDetails =
                await _rentalOrderDetailServices.GetPagedRentalOrderDetailsByUserId(userId, pageNumber, pageSize);

            if (rentalOrderDetails == null)
            {
                return NotFound(new { Message = "No rental order details found for the user." });
            }

            return Ok(rentalOrderDetails);
        }

        [HttpPut("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            try
            {
                string statusString = status switch
                {
                    OrderStatus.ChoXacNhan => "Chờ xác nhận",
                    OrderStatus.ChoGiaoHang => "Chờ giao hàng",
                    OrderStatus.DangVanChuyen => "Đang vận chuyển",
                    OrderStatus.DaHoanThanh => "Đã hoàn thành",
                    OrderStatus.DaHuy => "Đã hủy",
                    _ => throw new ArgumentOutOfRangeException()
                };

                var updatedOrder = await _rentalOrderServices.UpdateOrderStatus(orderId, statusString);
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("GetAllRentalOrders")]
        public async Task<ActionResult> GetAllRentalOrders()
        {
            var rentalOrders = await _rentalOrderServices.GetAllRentalOrdersAsync();
            return Ok(rentalOrders);
        }

        [HttpGet("GetRentalOrdersByUserId")]
        public async Task<IActionResult> GetRentalOrdersByUserId(int userId, int pageNumber, int pageSize)
        {
            var result = await _rentalOrderServices.GetRentalOrdersByUserIdAsync(userId, pageNumber, pageSize);
            return Ok(result);
        }

    }
}
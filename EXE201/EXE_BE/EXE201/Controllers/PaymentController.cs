using EXE201.BLL.Interfaces;
using EXE201.BLL.Services;
using EXE201.DAL.DTOs.PaymentDTOs;
using EXE201.DAL.DTOs.PaymentDTOs.EXE201.DAL.DTOs.PaymentDTOs;
using EXE201.DAL.Models;
using LMSystem.Repository.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EXE201.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentService;


        public PaymentController(IPaymentServices paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("AddPaymentForUser")]
        public async Task<IActionResult> AddPaymentForUser([FromQuery] int userId, [FromQuery] AddPaymentDTO request)
        {
            var response = await _paymentService.AddPaymentForUser(userId, request);
            if (response.Status == "Error")
            {
                return BadRequest(new { Message = response.Message });
            }
            return Ok(response.DataObject);
        }

        [HttpPost("ConfirmPayment")]
        public async Task<IActionResult> ConfirmPayment([FromQuery] int paymentId)
        {
            var response = await _paymentService.ConfirmPayment(paymentId);
            if (response.Status == "Error")
            {
                return BadRequest(new { Message = response.Message });
            }
            return Ok(response.DataObject);
        }

        [HttpGet("ViewHistoryPaymentByUserId")]
        public async Task<IActionResult> GetPaymentsByUserId(int userId, [FromQuery] PaginationParameter paginationParameter)
        {
            try
            {
                var result = await _paymentService.GetPaymentsByUserIdAsync(userId, paginationParameter);
                if (result == null || !result.Items.Any())
                {
                    return BadRequest("No payment history found for the specified user.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("ViewProfits")]
        //public async Task<IActionResult> ViewProfits([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        //{
        //    var profits = await _paymentService.GetProfitData(startDate, endDate);
        //    if (profits == null || !profits.Any())
        //    {
        //        return NotFound();
        //    }
        //    return Ok(profits);
        //}

        [HttpGet("ViewAllPaymentMethods")]
        public async Task<IActionResult> ViewAllPaymentMethods()
        {
            var paymentMethods = await _paymentService.GetAllPaymentMethods();
            return Ok(paymentMethods);
        }

        [HttpGet("ViewAllPayments")]
        public async Task<IActionResult> ViewAllPayments()
        {
            var payments = await _paymentService.GetAllPayments();
            return Ok(payments);
        }

        [HttpPost("CreatePaymentMethod")]
        public async Task<IActionResult> CreatePaymentMethod([FromQuery] string paymentMethodName)
        {
            var createdPaymentMethod = await _paymentService.CreatePaymentMethod(paymentMethodName);
            return CreatedAtAction(nameof(ViewAllPaymentMethods), new { id = createdPaymentMethod.PaymentMethodId }, createdPaymentMethod);
        }

        [HttpPatch("UpdatePaymentMethodName")]
        public async Task<IActionResult> UpdatePaymentMethodName([FromBody] UpdatePaymentMethodNameDTO updatePaymentMethodNameDTO)
        {
            var updatedPaymentMethod = await _paymentService.UpdatePaymentMethodName(updatePaymentMethodNameDTO.PaymentMethodId, updatePaymentMethodNameDTO.PaymentMethodName);
            if (updatedPaymentMethod == null)
            {
                return NotFound(new { Message = "Payment method not found." });
            }

            return Ok(updatedPaymentMethod);
        }

        [HttpDelete("DeletePaymentMethod/{paymentMethodId}")]
        public async Task<IActionResult> DeletePaymentMethod(int paymentMethodId)
        {
            var result = await _paymentService.DeletePaymentMethod(paymentMethodId);
            if (!result)
            {
                return NotFound(new { Message = "Payment method not found." });
            }

            return Ok(result);
        }

        [HttpDelete("DeletePayment/{paymentId}")]
        public async Task<IActionResult> DeletePayment(int paymentId)
        {
            var result = await _paymentService.DeletePayment(paymentId);
            if (!result)
            {
                return NotFound(new { Message = "Payment not found." });
            }

            return Ok(result);
        }

        [HttpDelete("CancelPayment/{paymentId}")]
        public async Task<IActionResult> CancelPayment(int paymentId)
        {
            var result = await _paymentService.CancelPaymentLink(paymentId);
            //if (!result)
            //{
            //    return NotFound(new { Message = "Payment not found." });
            //}

            return Ok(result);
        }
    }
}

using BusinessObjects.Dto;
using BusinessObjects.Dto.BillReqRes;
using BusinessObjects.Models;
using Management.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Tools;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BillController(IUserManagement userManagement, IPaymentService paymentService, ICustomerService customerService) : ControllerBase
{
    private IUserManagement UserManagement { get; } = userManagement;
    private IPaymentService PaymentService { get; } = paymentService;
    public ICustomerService CustomerService { get; } = customerService;

    [Authorize(Roles = "Admin, Manager, Staff")]
    [HttpGet("GetBills")]
    public async Task<IActionResult> Get(int pageNumber, int pageSize)
    {
        var bills = await UserManagement.GetBills(pageNumber, pageSize);
        return Ok(bills);
    }
    [Authorize(Roles = "Admin, Manager, Staff")]

    [HttpGet("GetBillById/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var bill = await UserManagement.GetBillById(id);
        if (bill == null) return NotFound();
        return Ok(bill);
    }
    [Authorize(Roles = "Admin, Manager, Staff")]

    [HttpPost("CreateBill")]
    public async Task<IActionResult> Create(BillRequestDto billRequestDto)
    {
        return Ok(await UserManagement.CreateBill(billRequestDto));
    }
    [Authorize(Roles = "Admin, Manager, Staff")]

    [HttpPost("CheckoutOnline/{billId}")]
    public async Task<IActionResult> CheckoutOnline(string billId,PaymentRequestDto paymentRequestDto)
    {
        var orderCode = Generator.GeneratePaymemtCode();
        var cancelUrl = Url.Action("CancelPayment","Bill",new {paymentRequestDto.ReturnUrl,orderCode},Request.Scheme);
        var suscessUrl = Url.Action("SuccessPayment","Bill",new {billId,paymentRequestDto.ReturnUrl,orderCode},Request.Scheme);
        return Ok(await PaymentService.CheckoutBill(billId,paymentRequestDto.Amount,orderCode,suscessUrl,cancelUrl));
    }
    [Authorize(Roles = "Admin, Manager, Staff")]

    [HttpPost("CheckoutOffline/{id}")]
    public async Task<IActionResult> CheckoutOffline(string id, float cashAmount)
    {
        var customer = await customerService.GetCustomerByBillId(id);
        await customerService.AddPoint(customer.CustomerId, 10);
        return Ok(await UserManagement.CheckoutBill(id,cashAmount));
    }

    [HttpGet("CancelPayment")]
    public async Task<IActionResult> CancelPayment(string returnUrl,long orderCode)
    {
        await paymentService.UpdatePaymentStatus(orderCode, PaymentStatus.Failed);
        return Redirect($"{returnUrl}/status=canceled");
    }
    [HttpGet("SuccessPayment")]
    public async Task<IActionResult> SuccessPayment(string billId,string returnUrl,long orderCode)
    {
        await paymentService.UpdatePaymentStatus(orderCode, PaymentStatus.Success);
        await paymentService.UpdateBillStatus(billId);
        await paymentService.UpdateJewelryStatus(billId);
        await PaymentService.CreatePurchase(billId);
        var customer = await customerService.GetCustomerByBillId(billId);
        await customerService.AddPoint(customer.CustomerId, 10);
        return Redirect($"{returnUrl}/status=success");
    }
}

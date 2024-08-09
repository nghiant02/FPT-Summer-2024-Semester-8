using BusinessObjects.Dto;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Net.payOS;
using Net.payOS.Types;
using Repositories.Interface;
using Services.Interface;
using Tools;

namespace Services.Implementation;

public class PaymentService : IPaymentService
{
    private IConfiguration _configuration;
    private readonly PayOS _payOs;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IBillRepository _billRepository;
    private readonly IBillDetailRepository _detailRepository;
    private readonly IJewelryRepository _jewelryRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    public PaymentService(IConfiguration configuration, IPurchaseRepository purchaseRepository, ICustomerRepository customerRepository, IUserRepository userRepository, IPaymentRepository paymentRepository, IBillRepository billRepository, IBillDetailRepository detailRepository, IJewelryRepository jewelryRepository)
    {
        _configuration = configuration;
        _paymentRepository = paymentRepository;
        _billRepository = billRepository;
        _detailRepository = detailRepository;
        _jewelryRepository = jewelryRepository;
        _customerRepository = customerRepository;
        _purchaseRepository = purchaseRepository;
        _userRepository = userRepository;
        _payOs = new PayOS(configuration["PayOs:ClientId"] ?? "", configuration["PayOs:ApiKey"] ?? "", configuration["PayOs:ChecksumKey"] ?? "");
    }

    public async Task<BillCheckoutResponse> CheckoutBill(string billId, int amount, long orderCode, string returnUrl, string cancelUrl)
    {
        var bill = await _billRepository.GetById(billId);

        if (bill == null) throw new Exception("Bill not found");

        if (bill.IsPaid == true) throw new Exception("Bill is already paid");

        var payment = new Payment
        {
            BillId = bill.BillId,
            PaymentId = "",
            Amount = amount,
            PaymentStatus = PaymentStatus.Pending,
            PaymentDate = DateTime.UtcNow.ToUniversalTime(),
            OrderCode = orderCode,
        };
        var paymentResult = await _paymentRepository.Create(payment);
        if (paymentResult == null) throw new Exception("Payment failed");


        var description = $"Thanh toán: ";
        var signature = "";

        var items = new List<ItemData>();
        var billDetails = await _detailRepository.GetBillDetail(billId);
        foreach (var item in billDetails.Items)
        {
            items.Add(new ItemData(item.Name, 1, (int)item.TotalPrice));
        }

        var paymentData = new PaymentData(orderCode, amount, description, items, cancelUrl, returnUrl, signature);
        var result = await _payOs.createPaymentLink(paymentData);
        var response = new BillCheckoutResponse
        {
            CheckoutUrl = result.checkoutUrl,
            OrderCode = orderCode
        };
        return response;
    }
    public async Task<bool> UpdatePaymentStatus(long orderCode, PaymentStatus paymentStatus)
    {
        var result = await _paymentRepository.UpdatePaymentStatus(orderCode, paymentStatus);
        return result != null;
    }

    public async Task<bool> UpdateBillStatus(string billId)
    {
        var bill = await _billRepository.GetById(billId);
        if (bill == null) throw new Exception("Bill not found");
        bill.IsPaid = true;
        return (await _billRepository.UpdateBill(bill) != 0);
    }

    public async Task<bool> UpdateJewelryStatus(string billId)
    {
        var billDetail = await _detailRepository.GetBillDetail(billId);
        if (billDetail == null) throw new Exception("Bill not found");
        foreach (var item in billDetail.Items)
        {
            var jewelry = await _jewelryRepository.GetJewelryById(item.JewelryId);
            if (jewelry == null) throw new Exception("Jewelry not found");
            jewelry.IsSold = true;
            await _jewelryRepository.Update(item.JewelryId, jewelry);
        }
        return true;
    }
    public async Task<bool> CreatePurchase(string billId)
    {
        var billDetail = await _detailRepository.GetBillDetail(billId);
        if (billDetail == null) throw new Exception("Bill not found");
        foreach (var item in billDetail.Items)
        {
            var purchase = new Purchase
            {
                PurchaseId = Generator.GenerateId(),
                BillId = billDetail.BillId,
                IsBuyBack = 0,
                CustomerId = await _customerRepository.GetCustomerIdByName(billDetail.CustomerName),
                UserId = await _userRepository.GetUserIdByName(billDetail.StaffName),
                PurchaseDate = DateTime.UtcNow.ToUniversalTime(),
                PurchasePrice = item.TotalPrice,
                JewelryId = item.JewelryId
            };
            await _purchaseRepository.CreatePurchase(purchase);
        };
        return true;
    }
}

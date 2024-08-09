using BusinessObjects.Dto;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;

namespace Services.Interface;

public interface IPaymentService
{
    Task<BillCheckoutResponse> CheckoutBill(string billId, int amount,long orderCode, string returnUrl, string cancelUrl);
    Task<bool> UpdatePaymentStatus(long orderCode, PaymentStatus paymentStatus);
    Task<bool> UpdateBillStatus(string billId);
    Task<bool> UpdateJewelryStatus(string billId);
    Task<bool> CreatePurchase(string billId);
}
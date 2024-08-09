using Net.payOS.Types;
using Net.payOS;
using System.Collections.Generic;
using System.Threading.Tasks;
using EXE201.BLL.Interfaces;

public class PayOSPaymentService {
    private readonly PayOS _payOS;

    public PayOSPaymentService(string clientId, string apiKey, string checksumKey)
    {
        _payOS = new PayOS(clientId, apiKey, checksumKey);
    }

    public async Task<CreatePaymentResult> CreatePaymentLink(PaymentData paymentData)
    {
        return await _payOS.createPaymentLink(paymentData);
    }

    public async Task<PaymentLinkInformation> GetPaymentLinkInformation(int orderId)
    {
        return await _payOS.getPaymentLinkInformation(orderId);
    }

    public async Task<PaymentLinkInformation> CancelPaymentLink(int paymentId)
    {
        return await _payOS.cancelPaymentLink(paymentId);
    }

    public async Task<string> ConfirmWebhook(string webhookUrl)
    {
        return await _payOS.confirmWebhook(webhookUrl);
    }

    public WebhookData VerifyPaymentWebhookData(WebhookType webhookBody)
    {
        return _payOS.verifyPaymentWebhookData(webhookBody);
    }
}

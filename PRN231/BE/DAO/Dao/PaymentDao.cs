using BusinessObjects.Context;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace DAO.Dao;

public class PaymentDao
{
    private readonly JssatsContext _context;

    public PaymentDao()
    {
        _context = new JssatsContext();
    }
    public async Task<Payment> CreatePayment(Payment payment)
    {
        payment.PaymentId = Generator.GenerateId();
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
        return payment;
    }
    public async Task<Payment?> GetPaymentById(int id)
    {
        return await _context.Payments.FindAsync(id);
    }
    public async Task<List<Payment>> GetPayments()
    {
        return await _context.Payments.ToListAsync();
    }
    public async Task<Payment?> GetPaymentByBillId(string billId)
    {
       return await _context.Payments.FirstOrDefaultAsync(x => x.BillId == billId);
    }

    public async Task<Payment?> UpdatePaymentStatus(long orderCode, PaymentStatus paymentStatus)
    {
        var payment = await _context.Payments.FirstOrDefaultAsync(x => x.OrderCode == orderCode);
        if (payment == null) return payment;
        payment.PaymentStatus = paymentStatus;
        await _context.SaveChangesAsync();
        return payment;
    }
    public async Task<Payment?> GetPaymentByOrderCode(long orderCode)
    {
        return await _context.Payments.FirstOrDefaultAsync(x => x.OrderCode == orderCode);
    }
}
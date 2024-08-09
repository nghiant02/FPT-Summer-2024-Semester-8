using BusinessObjects.Models;
using DAO.Dao;
using MongoDB.Driver;
using Repositories.Interface;

namespace Repositories.Implementation;

public class PaymentRepository : IPaymentRepository
{ 
    private readonly PaymentDao _paymentDao;

    public PaymentRepository(PaymentDao paymentDao)
    {
        _paymentDao = paymentDao;
    }

    public async Task<Payment> Create(Payment entity)
    {
        return await _paymentDao.CreatePayment(entity);
    }

    public async Task<Payment?> UpdatePaymentStatus(long orderCode, PaymentStatus paymentStatus)
    {
        return await _paymentDao.UpdatePaymentStatus(orderCode, paymentStatus);
    }

    public async Task<Payment?> GetPaymentByBillId(string billId)
    {
        return await _paymentDao.GetPaymentByBillId(billId);
    }
    public async Task<Payment?> GetPaymentByOrderCode(long orderCode)
    {
        return await _paymentDao.GetPaymentByOrderCode(orderCode);
    }
}
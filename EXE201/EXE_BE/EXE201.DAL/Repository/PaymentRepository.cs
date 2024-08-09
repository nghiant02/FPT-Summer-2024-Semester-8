using EXE201.DAL.DTOs;
using EXE201.DAL.DTOs.PaymentDTOs;
using EXE201.DAL.DTOs.PaymentDTOs.EXE201.DAL.DTOs.PaymentDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using LMSystem.Repository.Helpers;
using MCC.DAL.Repository.Implements;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly EXE201Context _context;

        public PaymentRepository(EXE201Context context) : base(context)
        {
            _context = context;
        }
        public async Task<ResponeModel> AddPaymentForUser(int userId, AddPaymentDTO paymentDetails)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return new ResponeModel { Status = "Error", Message = "User not found" };
            }

            var cartItems = await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return new ResponeModel { Status = "Error", Message = "No items in cart" };
            }

            var paymentAmount = cartItems.Sum(c => (decimal?)(c.Quantity * c.Product.ProductPrice ?? 0));

            var payment = new Payment
            {
                UserId = userId,
                PaymentAmount = paymentAmount,
                FullName = paymentDetails.FullName ?? user.FullName,
                Phone = paymentDetails.Phone ?? user.Phone,
                Address = paymentDetails.Address ?? user.Address,
                PaymentStatus = "Pending",
                PaymentTime = DateTime.UtcNow,
                PaymentMethodId = paymentDetails.PaymentMethodId
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // Ensure PaymentMethod is included
            var paymentWithMethod = await _context.Payments
                .Include(p => p.PaymentMethod)  // Ensure PaymentMethod is included
                .Include(p => p.User)  // Include User for cart details
                .ThenInclude(u => u.Carts)
                .ThenInclude(c => c.Product)
                .Where(p => p.PaymentId == payment.PaymentId)
                .FirstOrDefaultAsync();

            if (paymentWithMethod == null)
            {
                return new ResponeModel { Status = "Error", Message = "Payment retrieval failed after creation" };
            }

            return new ResponeModel { Status = "Success", Message = "Payment created successfully", DataObject = paymentWithMethod };
        }

        public async Task UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task ClearUserCart(int userId)
        {
            var cartItems = await _context.Carts
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (cartItems.Any())
            {
                _context.Carts.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ResponeModel> ConfirmPayment(int paymentId)
        {
            var payment = await _context.Payments
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);

            if (payment == null)
            {
                return new ResponeModel { Status = "Error", Message = "Payment not found" };
            }

            if (payment.PaymentStatus == "Completed")
            {
                return new ResponeModel { Status = "Error", Message = "Payment is already completed" };
            }

            payment.PaymentStatus = "Completed";
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();

            await ClearUserCart((int)payment.UserId);

            return new ResponeModel { Status = "Success", Message = "Payment confirmed and cart cleared successfully" };
        }

        public async Task<PagedResponseDTO<PaymentHistoryDto>> GetPaymentHistoryByUserIdAsync(int userId, PaginationParameter paginationParameter)
        {
            var query = _dbSet
                            .Where(p => p.UserId == userId)
                            .Include(p => p.Order)
                                .ThenInclude(o => o.RentalOrderDetails)
                                    .ThenInclude(ro => ro.Product) // Include product details
                            .OrderByDescending(p => p.PaymentTime);

            var totalRecords = await query.CountAsync();
            var payments = await query
                .Skip((paginationParameter.PageNumber - 1) * paginationParameter.PageSize)
                .Take(paginationParameter.PageSize)
                .Select(p => new PaymentHistoryDto
                {
                    PaymentId = p.PaymentId,
                    PaymentTime = p.PaymentTime,
                    PaymentAmount = p.PaymentAmount,
                    PaymentContent = p.Order.RentalOrderDetails.Select(ro => ro.Product.ProductTitle).FirstOrDefault(),
                    PaymentStatus = p.PaymentStatus,
                    PaymentMethodName = p.PaymentMethod.PaymentMethodName
                })
                .ToListAsync();

            return new PagedResponseDTO<PaymentHistoryDto>
            {
                PageNumber = paginationParameter.PageNumber,
                PageSize = paginationParameter.PageSize,
                TotalCount = totalRecords,
                Items = payments
            };
        }

        //public async Task<IEnumerable<ProfitDTO>> GetProfitData(DateTime startDate, DateTime endDate)
        //{
        //    var profits = await _context.Payments
        //        .Where(p => p.PaymentStatus == "Completed" && p.Order.DatePlaced >= startDate && p.Order.DatePlaced <= endDate)
        //        .GroupBy(p => p.Order.DatePlaced.Value.Date)
        //        .Select(g => new ProfitDTO
        //        {
        //            Date = g.Key,
        //            TotalProfit = g.Sum(p => p.PaymentAmount ?? 0)
        //        })
        //        .ToListAsync();

        //    return profits;
        //}

        public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethods()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<PaymentMethod> CreatePaymentMethod(string paymentMethodName)
        {
            var paymentMethod = new PaymentMethod
            {
                PaymentMethodName = paymentMethodName
            };

            _context.PaymentMethods.Add(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethod;
        }

        public async Task<PaymentMethod> UpdatePaymentMethodName(int paymentMethodId, string paymentMethodName)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(paymentMethodId);
            if (paymentMethod == null)
            {
                return null;
            }

            paymentMethod.PaymentMethodName = paymentMethodName;

            _context.PaymentMethods.Update(paymentMethod);
            await _context.SaveChangesAsync();

            return paymentMethod;
        }

        public async Task<bool> DeletePaymentMethod(int paymentMethodId)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(paymentMethodId);
            if (paymentMethod == null)
            {
                return false;
            }

            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePayment(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
            {
                return false;
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
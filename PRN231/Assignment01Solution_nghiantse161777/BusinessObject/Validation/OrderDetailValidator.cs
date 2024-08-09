using BusinessObject.Models;
using System;

namespace Validation
{
    public class OrderDetailValidator
    {
        public void Validate(OrderDetail orderDetail)
        {
            if (!IsValid(orderDetail))
            {
                throw new ArgumentException("Invalid order detail. Quantity must be positive and discount must be between 0 and 1.");
            }
        }

        private bool IsValid(OrderDetail orderDetail)
        {
            return orderDetail.Quantity > 0 && orderDetail.Discount >= 0 && orderDetail.Discount <= 1;
        }
    }
}

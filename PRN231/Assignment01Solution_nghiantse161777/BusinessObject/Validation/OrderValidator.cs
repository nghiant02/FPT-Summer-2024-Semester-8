using BusinessObject.Models;
using System;

namespace Validation
{
    public class OrderValidator
    {
        public void Validate(Order order)
        {
            if (!IsValidOrderDate(order.OrderDate))
            {
                throw new ArgumentException("Order date cannot be in the future.");
            }
            if (!IsValidFreight(order.Freight))
            {
                throw new ArgumentException("Freight must be non-negative.");
            }
        }

        private bool IsValidOrderDate(DateTime orderDate)
        {
            return orderDate <= DateTime.Now;
        }

        private bool IsValidFreight(decimal freight)
        {
            return freight >= 0;
        }
    }
}

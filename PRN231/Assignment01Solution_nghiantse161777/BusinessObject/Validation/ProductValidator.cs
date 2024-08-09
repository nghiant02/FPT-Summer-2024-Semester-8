using BusinessObject.Models;
using System;

namespace Validation
{
    public class ProductValidator
    {
        public void Validate(Product product)
        {
            if (!IsValid(product))
            {
                throw new ArgumentException("Invalid product details. Unit price and units in stock must be non-negative.");
            }
        }

        private bool IsValid(Product product)
        {
            return product.UnitPrice >= 0 && product.UnitsInStock >= 0;
        }
    }
}

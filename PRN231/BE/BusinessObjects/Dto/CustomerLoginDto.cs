using System;
using System.Text.RegularExpressions;

namespace BusinessObjects.Dto
{
    public class CustomerLoginDto
    {
        private string? _phone;

        public string? Phone
        {
            get => _phone;
            set
            {
                if (value != null && !IsValidPhoneNumber(value))
                {
                    throw new ArgumentException("Invalid phone number format.");
                }
                _phone = value;
            }
        }

        public string? Password { get; set; }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Updated pattern to match a 10-digit phone number
            var pattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
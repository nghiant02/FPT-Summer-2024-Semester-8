using System;
using System.Text.RegularExpressions;

namespace BusinessObjects.Dto
{
    public class CustomerRegisterDto
    {
        private string? _phone;

        public required string? Phone
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

        public required string Password { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Updated pattern to match a 10-digit phone number
            var pattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
using BusinessObject.Models;
using System;
using System.Net.Mail;

namespace Validation
{
    public class MemberValidator
    {
        public void Validate(Member member)
        {
            if (!IsValidEmail(member.Email))
            {
                throw new ArgumentException("Invalid email format.");
            }
            if (!IsValidPassword(member.Password))
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            return password != null && password.Length >= 6;
        }
    }
}

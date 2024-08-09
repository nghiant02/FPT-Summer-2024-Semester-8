using EXE201.BLL.Interfaces;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Tools;

namespace EXE201.BLL.Services
{
    public class ForgotPasswordService : IForgotPawwordService
    {
        private readonly IVerifyCodeRepository _verifyCodeRepository;
        private readonly IUserRepository _userRepository;
        public ForgotPasswordService(IVerifyCodeRepository verifyCodeRepository, IUserRepository userRepository)

        {
            _verifyCodeRepository = verifyCodeRepository;
            _userRepository = userRepository;
        }
        public async Task<VerifyCode> AddCode(string code, string email)
        {
            var verifyCode = new VerifyCode
            {
                Id = IdGenerator.GenerateId(),
                Email = email,
                Code = code,
                CreatedAt = DateTime.Now,
            }; 
            await _verifyCodeRepository.AddAsync(verifyCode);
            await _verifyCodeRepository.SaveChangesAsync();
            return verifyCode;
        }

        public async Task<bool> Delete(string email)
        {
            IEnumerable<VerifyCode> verifyCodes = await _verifyCodeRepository.FindAsync(x => x.Email == email);
            var id  = verifyCodes.FirstOrDefault();
            if (id == null)
            {
                return false;
            }
            await _verifyCodeRepository.Delete(id);
            return true;

        }

        public async Task<bool> IsCodeExist(string email)
        {
            IEnumerable<VerifyCode> verifyCodes = await _verifyCodeRepository.FindAsync(x => x.Email == email);
            if (!verifyCodes.Any())
            {
                return false;
            }
            return true;
        }

        public async Task<User> VerifyCode(string code, string email)
        {
            var users = await _userRepository.FindAsync(x => x.Email == email);

            if (!users.Any())
            {
                throw new ArgumentException("Email is invalid");
            }

            var verifyCode = await _verifyCodeRepository.FindAsync(x => x.Email == email && x.Code == code);
            if (!verifyCode.Any())
            {
                throw new ArgumentException("Verify code is invalid");
            }

            var createAt = verifyCode.First().CreatedAt;

            DateTime now = DateTime.UtcNow;
            TimeSpan? timeDifference = now - createAt;

            if (timeDifference.HasValue && timeDifference.Value.TotalSeconds < 60)
            {
                return users.First();
            }
            else
            {
                throw new ArgumentException("Verify code expired");
            }
        }

    }
}

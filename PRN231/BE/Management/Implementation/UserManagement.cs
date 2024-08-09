using BusinessObjects.Dto;
using BusinessObjects.Dto.BillReqRes;
using BusinessObjects.Dto.Counter;
using BusinessObjects.Dto.Other;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;
using Management.Interface;
using Services.Interface;

namespace Management.Implementation
{
    public class UserManagement(IJewelryService jewelryService,IUserService userService, IBillService billService, ITokenService tokenService) : IUserManagement
    {
        public IJewelryService JewelryService { get; } = jewelryService;
        private IUserService UserService { get; } = userService;
        private IBillService BillService { get; } = billService;
        private ITokenService TokenService { get; } = tokenService;

        public async Task<TokenResponseDto?> Login(LoginDto loginDto)
        {
            var user = await UserService.Login(loginDto);
            if (user == null) return null;
            var token = await TokenService.CreateToken(user);
            return token;
        }
        public async Task<TokenResponseDto?> CustomerLogin(CustomerLoginDto customerLoginDto)
        {
            var customer = await UserService.LoginCustomer(customerLoginDto);
            if (customer == null) return null;
            var token = await TokenService.CreateToken(customer);
            return token;
        } 
        public async Task<int?> Logout(string id)
        {
            var result = await UserService.Logout(id);
            return 1;
        }

        public async Task<BillCashCheckoutResponseDto> CheckoutBill(string id, float cashAmount)
        {
            return await BillService.CheckoutBill(id, cashAmount);
        }
        public async Task<IEnumerable<UserResponseDto?>?> GetUsers()
        {
            return await UserService.GetUsers();
        }

        public async Task<PagingResponse> GetBills(int pageNumber, int pageSize)
        {
            return await BillService.GetBills(pageNumber, pageSize);
        }

        public async Task<BillDetailDto?> GetBillById(string id)
        {
            return await BillService.GetById(id);
        }
        
        public async Task<BillResponseDto> CreateBill(BillRequestDto billRequestDto)
        {
            return await BillService.Create(billRequestDto);
        }

        public async Task<UserResponseDto?> GetUserById(string id)
        {
            return await UserService.GetUserById(id);
        }

        public async Task<int> AddUser(UserDto userDto)
        {
            return await UserService.AddUser(userDto);
        }

        public async Task<int> UpdateUser(string id, UserDto user)
        {
            return await UserService.UpdateUser(id, user);
        }

        public async Task<int> DeleteUser(string id)
        {
            return await UserService.DeleteUser(id);
        }
    }
}
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Dto;
using BusinessObjects.Dto.Other;
using BusinessObjects.Dto.ResponseDto;

namespace Services.Interface
{
    public interface ICustomerService
    {
        public Task<PagingResponse> GetCustomersPaging(int pageNumber, int pageSize);
        public Task<CustomerResponseDto?> GetCustomerById(string id);
        public Task<CustomerResponseDto?> GetCustomerByPhone(string phoneNumber);

        public Task<CustomerResponseDto> CreateCustomer(CustomerDto customer);
        public Task<int> UpdateCustomer(string id,CustomerDto customer);
        public Task<int> DeleteCustomer(string id);
        Task<bool> RegisterCustomer(CustomerRegisterDto customerRegisterDto);
        Task<int> AddPoint(string customerId, int point);
        Task<Customer?> GetCustomerByBillId(string? billId);

    }
}

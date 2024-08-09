using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Dto.Other;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation
{
    public class CustomerService(IMapper mapper, ICustomerRepository customerRepository) : ICustomerService
    {
        private IMapper Mapper { get; } = mapper;
        private ICustomerRepository CustomerRepository { get; } = customerRepository;

        public async Task<CustomerResponseDto> CreateCustomer(CustomerDto customerDto)
        {
            var customer = Mapper.Map<Customer>(customerDto);
            var result = await CustomerRepository.CreateCustomer(customer);
            var responseCustomer = Mapper.Map<CustomerResponseDto>(result);
            return responseCustomer;
        }

        public async Task<int> DeleteCustomer(string id)
        {
            return await CustomerRepository.Delete(id);
        }
        
        public async Task<CustomerResponseDto?> GetCustomerByPhone(string phoneNumber)
        {
            var customer = await CustomerRepository.GetCustomerByPhone(phoneNumber);
            var responseCustomer = Mapper.Map<CustomerResponseDto>(customer);
            return responseCustomer;
        }

        public async Task<PagingResponse> GetCustomersPaging(int pageNumber, int pageSize)
        {
            var customers = await CustomerRepository.GetsPaging(pageNumber, pageSize);
            var pagingResponse = new PagingResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecord = customers.Item1,
                TotalPage = customers.Item2,
                Data = Mapper.Map<IEnumerable<CustomerResponseDto>>(customers.Item3)
            };
            return pagingResponse;
        }

        public async Task<CustomerResponseDto?> GetCustomerById(string id)
        {
            var customer = await CustomerRepository.GetById(id);
            var responseCustomer = Mapper.Map<CustomerResponseDto>(customer);
            return responseCustomer;
        }

        public async Task<IEnumerable<CustomerResponseDto?>?> GetCustomers()
        {
            var customers = await CustomerRepository.Gets();
            var responseCustomers = Mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
            return responseCustomers;
        }

        public async Task<int> UpdateCustomer(string id, CustomerDto customerDto)
        {
            var customer = Mapper.Map<Customer>(customerDto);
            return await CustomerRepository.Update(id, customer);
        }
        public async Task<bool> RegisterCustomer(CustomerRegisterDto customerRegisterDto)
        {
            var customer = Mapper.Map<Customer>(customerRegisterDto);
            return await CustomerRepository.RegisterCustomer(customer);
        }

        public Task<int> AddPoint(string customerId, int point)
        {
            return CustomerRepository.AddPoint(customerId, point);
        }

        public Task<Customer?> GetCustomerByBillId(string? billId)
        {
            return CustomerRepository.GetCustomerByBillId(billId);
        }
    }
}
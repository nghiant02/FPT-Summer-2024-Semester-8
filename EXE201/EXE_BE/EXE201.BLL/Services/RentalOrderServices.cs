using AutoMapper;
using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs;
using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.DTOs.RentalOrderDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Services
{
    public class RentalOrderServices : IRentalOrderServices
    {
        private readonly IRentalOrderRepository _rentalOrderRepository;
        private readonly IMapper _mapper;

        public RentalOrderServices(IRentalOrderRepository rentalOrderRepository, IMapper mapper)
        {
            _rentalOrderRepository = rentalOrderRepository;
            _mapper = mapper;
        }
        //public async Task<ResponeModel> CancelOrderAsync(int orderId)
        //{
        //    return await _rentalOrderRepository.CancelOrderAsync(orderId);
        //}

        //public async Task<ResponeModel> ReturnOrderAsync(int orderId, string returnReason)
        //{
        //    return await _rentalOrderRepository.ReturnOrderAsync(orderId, returnReason);
        //}

        //public async Task<ResponeModel> ReturnItem(ReturnItemDTO returnItem)
        //{
        //    return await _rentalOrderRepository.ReturnItem(returnItem);
        //}

        //public async Task<OrderStatusDTO> GetOrderStatus(int orderId)
        //{
        //    return await _rentalOrderRepository.GetOrderStatus(orderId);
        //}

        //public async Task<PagedResponseDTO<RentalOrderResponseDTO>> GetRentalOrdersByUserId(int userId, int PageNumber, int PageSize)
        //{
        //    return await _rentalOrderRepository.GetRentalOrdersByUserId(userId, PageNumber, PageSize);
        //}
        //public async Task<RentalOrderResponseDTO> GetRentalOrderByUserId(int userId)
        //{
        //    return await _rentalOrderRepository.GetRentalByUserId(userId);
        //}

        public async Task<PagingResponse> GetRentalOrdersByStatus(OrderStatus? status, int pageNumber, int pageSize)
        {
            var listRentalOrder = await _rentalOrderRepository.RentalOrdersByStatus(status, pageNumber, pageSize);
            var rentalOrders = new PagingResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecord = listRentalOrder.Item1,
                TotalPage = listRentalOrder.Item2,
                Data = listRentalOrder.Item3
            };
            return rentalOrders;
        }

        public async Task<PagingResponse> GetRentalOrders(int pageNumber, int pageSize)
        {
            var listRentalOrder = await _rentalOrderRepository.RentalOrders(pageNumber, pageSize);
            var rentalOrders = new PagingResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecord = listRentalOrder.Item1,
                TotalPage = listRentalOrder.Item2,
                Data = listRentalOrder.Item3
            };
            return rentalOrders;
        }

        public async Task<PagingResponse> GetReturnOrders(int pageNumber, int pageSize)
        {
            var listReturnOrder = await _rentalOrderRepository.ReturnOrders(pageNumber, pageSize);
            var returnOrders = new PagingResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecord = listReturnOrder.Item1,
                TotalPage = listReturnOrder.Item2,
                Data = listReturnOrder.Item3
            };
            return returnOrders;
        }

        public async Task<RentalOrder> UpdateOrderStatus(int orderId, string status)
        {
            var validStatuses = new List<string> { "Chờ xác nhận", "Chờ giao hàng", "Đang vận chuyển", "Đã hoàn thành", "Đã hủy" };
            if (!validStatuses.Contains(status))
            {
                throw new Exception("Invalid status");
            }

            return await _rentalOrderRepository.UpdateOrderStatus(orderId, status);
        }

        public async Task<IEnumerable<RentalOrder>> GetAllRentalOrdersAsync()
        {
            return await _rentalOrderRepository.GetAllRentalOrdersAsync();
        }

        public async Task<PagingResponse> GetRentalOrdersByUserIdAsync(int userId, int pageNumber, int pageSize)
        {
            var result = await _rentalOrderRepository.GetRentalOrdersByUserIdAsync(userId, pageNumber, pageSize);
            return new PagingResponse
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecord = result.Item1,
                TotalPage = result.Item2,
                Data = result.Item3.ToList()
            };
        }

    }
}
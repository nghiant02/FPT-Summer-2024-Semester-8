using EXE201.DAL.DTOs;
using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.DTOs.RentalOrderDTOs;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Interfaces
{
    public interface IRentalOrderServices
    {
        //Task<ResponeModel> CancelOrderAsync(int orderId);
        //Task<ResponeModel> ReturnOrderAsync(int orderId, string returnReason);
        //Task<ResponeModel> ReturnItem(ReturnItemDTO returnItem);
        //Task<OrderStatusDTO> GetOrderStatus(int orderId);
        //Task<PagedResponseDTO<RentalOrderResponseDTO>>
        //GetRentalOrdersByUserId(int userId, int PageNumber, int PageSize);
        //Task<RentalOrderResponseDTO> GetRentalOrderByUserId(int userId);
        Task<PagingResponse> GetRentalOrdersByStatus(OrderStatus? status, int pageNumber, int pageSize);
        Task<PagingResponse> GetRentalOrders(int pageNumber, int pageSize);
        Task<PagingResponse> GetReturnOrders(int pageNumber, int pageSize);
        Task<RentalOrder> UpdateOrderStatus(int orderId, string status);
        Task<IEnumerable<RentalOrder>> GetAllRentalOrdersAsync();
        Task<PagingResponse> GetRentalOrdersByUserIdAsync(int userId, int pageNumber, int pageSize);
    }
}
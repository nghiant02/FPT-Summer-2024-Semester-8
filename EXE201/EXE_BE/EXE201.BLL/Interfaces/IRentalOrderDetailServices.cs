using EXE201.DAL.DTOs.RentalOrderDTOs;
using EXE201.DAL.DTOs;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Interfaces
{
    public interface IRentalOrderDetailServices
    {
        Task<RentalOrderDetail> GetRentalOrderDetailById(int id);
        Task<PagingResponse> GetRentalOrderByStaff(int pageNumber, int pageSize, OrderStatus? status = null);
        Task<PagedResponseDTO<RentalOrderDetailResponseDTO>> GetPagedRentalOrderDetailsByUserId(int userId, int pageNumber, int pageSize);
    }
}

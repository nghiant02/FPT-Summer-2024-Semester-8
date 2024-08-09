using EXE201.DAL.DTOs.RentalOrderDTOs;
using EXE201.DAL.DTOs;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.Interfaces
{
    public interface IRentalOrderDetailRepository : IGenericRepository<RentalOrderDetail>
    {
        Task<RentalOrderDetail> UpdateRentalDetail(RentalOrderDetail rentalOrderDetail);
        Task<RentalOrderDetail> GetRentalOrderDetail(int id);
        Task<PagedResponseDTO<RentalOrderDetailResponseDTO>> GetPagedRentalOrderDetailsByUserId(int userId, int pageNumber, int pageSize);
        Task<(int, int, IEnumerable<ViewRentalOrderDetail>)> GetRentalOrderByStaff(int pageNumber, int pageSize, OrderStatus? status = null);
    }
}
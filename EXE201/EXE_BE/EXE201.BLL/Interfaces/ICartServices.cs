using EXE201.DAL.DTOs.CartDTOs;
using EXE201.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.BLL.Interfaces
{
    public interface ICartServices
    {
        Task<IEnumerable<ViewCartDto>> GetAllCarts();
        Task<IEnumerable<ViewCartDto>> GetCartById(int userId);
        Task<ViewCartDto> UpdateCartByUserId(int userId, UpdateCartDto cart);
        Task<bool> DeleteCart(int id);
        Task<Cart> AddNewCart(AddNewCartDTO cart);
    }
}

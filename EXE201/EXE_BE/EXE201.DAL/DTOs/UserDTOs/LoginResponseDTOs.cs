using EXE201.BLL.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.UserDTOs
{
    public class LoginResponseDTOs
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Expired { get; set; }
    }
}

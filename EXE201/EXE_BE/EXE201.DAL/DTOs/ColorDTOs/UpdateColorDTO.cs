using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.ColorDTOs
{
    public class UpdateColorDTO
    {
        public int ColorId { get; set; }
        public string NewColorName { get; set; }
        public string NewHexCode {  get; set; }
    }

}

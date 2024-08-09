using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.EmailDTOs
{
    public class EmailView
    {
        public required string To { get; set; }
        public required string Name { get; set; }
        public required string Id { get; set; }
    }
}

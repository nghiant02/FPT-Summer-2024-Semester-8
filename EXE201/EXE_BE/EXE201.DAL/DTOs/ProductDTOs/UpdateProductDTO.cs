using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201.DAL.DTOs.ProductDTOs
{
    public class UpdateProductDTO
    {
        [Required(ErrorMessage = "Id is required!")]
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public string? ProductTitle { get; set; }

        public string? Description { get; set; }

        public IEnumerable<string>? ProductImage { get; set; }

        public double? Price { get; set; }

        public int? CategoryId { get; set; }

        public IEnumerable<string>? ProductColor { get; set; }

        public IEnumerable<string>? ProductSize { get; set; }
    }

}

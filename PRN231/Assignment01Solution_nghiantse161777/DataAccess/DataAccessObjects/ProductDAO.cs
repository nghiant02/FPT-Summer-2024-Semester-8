using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private readonly eStoreDBContext _context;
        private readonly IMapper _mapper;

        public ProductDAO(eStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync() =>
            _mapper.Map<IEnumerable<ProductDTO>>(await _context.Products.ToListAsync());

        public async Task<ProductDTO> GetProductByIdAsync(int productId) =>
            _mapper.Map<ProductDTO>(await _context.Products.FindAsync(productId));

        public async Task AddProductAsync(ProductCreateUpdateDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(int productId, ProductCreateUpdateDTO productDTO)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _mapper.Map(productDTO, product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using AutoMapper;
using BussiniseObject.Dto;
using BussiniseObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    public ProductController(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    [HttpGet("GetProducts")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _repository.GetProducts();
        return Ok(products);
    }
    [HttpGet("GetProduct/{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _repository.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct(ProductDto product)
    {
        var mappedProduct = _mapper.Map<Product>(product);
        var newProduct = await _repository.CreateProduct(mappedProduct);
        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.ProductId }, newProduct);
    }
    [HttpPut("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto product)
    {
        var mappedProduct = _mapper.Map<Product>(product);
        var updatedProduct = await _repository.UpdateProduct(id, mappedProduct);
        if (updatedProduct == null)
        {
            return NotFound();
        }
        return Ok(updatedProduct);
    }
    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deletedProduct = await _repository.DeleteProduct(id);
        if (deletedProduct == null)
        {
            return NotFound();
        }
        return Ok(deletedProduct);
    }
}
using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs.CategoryDTOs;
using EXE201.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryServices _categoryService;

    public CategoryController(ICategoryServices categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("GetAllCategories")]
    public async Task<ActionResult> GetAllCategories()
    {
        try
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetCategoryById")]
    public async Task<ActionResult> GetCategoryById(int categoryId)
    {
        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetCategoriesByStatus")]
    public async Task<ActionResult> GetCategoriesByStatus(string status)
    {
        try
        {
            var categories = await _categoryService.GetCategoriesByStatusAsync(status);
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("AddCategory")]
    public async Task<ActionResult> AddCategory([FromBody] AddCategoryDTOs categoryDto)
    {
        try
        {
            await _categoryService.AddCategoryAsync(categoryDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateCategory")]
    public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryDTOs categoryDto)
    {
        try
        {
            await _categoryService.UpdateCategoryAsync(categoryDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateCategoryStatus")]
    public async Task<ActionResult> UpdateCategoryStatus(int categoryId, [FromBody] string status)
    {
        try
        {
            await _categoryService.UpdateCategoryStatusAsync(categoryId, status);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

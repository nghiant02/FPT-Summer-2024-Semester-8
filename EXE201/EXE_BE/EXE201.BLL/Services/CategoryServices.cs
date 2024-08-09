using AutoMapper;
using EXE201.BLL.Interfaces;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using EXE201.DAL.DTOs.CategoryDTOs;
using System.ComponentModel.DataAnnotations;

public class CategoryServices : ICategoryServices
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) throw new KeyNotFoundException("Category not found");
        return category;
    }

    public async Task AddCategoryAsync(AddCategoryDTOs categoryDto)
    {
        await ValidateCategoryNameExists(categoryDto.CategoryName);
        await ValidateCategoryStatus(categoryDto.CategoryStatus);

        var category = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(UpdateCategoryDTOs categoryDto)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryDto.CategoryId);
        if (category == null) throw new KeyNotFoundException("Category not found");

        await ValidateCategoryNameExists(categoryDto.CategoryName);
        await ValidateCategoryStatus(categoryDto.CategoryStatus);

        _mapper.Map(categoryDto, category);
        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task UpdateCategoryStatusAsync(int id, string status)
    {
        await ValidateCategoryStatus(status);

        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null) throw new KeyNotFoundException("Category not found");

        category.CategoryStatus = status;
        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetCategoriesByStatusAsync(string status)
    {
        await ValidateCategoryStatus(status);

        return await _categoryRepository.GetCategoriesByStatusAsync(status);
    }

    private async Task ValidateCategoryStatus(string status)
    {
        if (status != "Active" && status != "Inactive" && status != "Archived")
        {
            throw new ValidationException("Invalid status");
        }
    }

    private async Task ValidateCategoryNameExists(string categoryName, int? categoryId = null)
    {
        var existingCategory = await _categoryRepository.GetByNameAsync(categoryName);
        if (existingCategory != null && (!categoryId.HasValue || existingCategory.CategoryId != categoryId.Value))
        {
            throw new ValidationException("Category name already exists");
        }
    }
}

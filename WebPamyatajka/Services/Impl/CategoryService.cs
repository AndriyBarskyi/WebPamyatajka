using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebPamyatajka.Data;
using WebPamyatajka.Models;

namespace WebPamyatajka.Services.Impl;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CategoryService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public List<Category> GetAllByCreatorIdOrDefault()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return _context.Categories.Where(c => c.IsDefault).ToList();
        }

        return _context.Categories.Where(c => c.CreatorId == userId).ToList();
    }

    public Category GetById(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException("Id must be greater than zero.");
        }

        if (_httpContextAccessor.HttpContext == null)
        {
            return _context.Categories.FirstOrDefault(c => c.IsDefault);
        }

        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Category category = _context.Categories.FirstOrDefault(c => c.Id == id && c.CreatorId == userId);
        if (category == null)
        {
            category = _context.Categories.FirstOrDefault(c => c.Id == id && c.IsDefault);
        }

        return category;
    }

    public Category Create(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }
        HttpContext? httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            category.IsDefault = true;
        }
        if (string.IsNullOrEmpty(category.Name))
        {
            throw new ArgumentException("Name of the category is required.");
        }

        var userId = httpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId != null)
        {
            category.CreatorId = userId;
        }
        _context.Categories.Add(category);
        _context.SaveChanges();
        return category;
    }

    public void Update(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        if (string.IsNullOrEmpty(category.Name))
        {
            throw new ArgumentException("Name of the category is required.");
        }

        _context.Categories.Update(category);
        _context.SaveChanges();
    }

    public void DeleteById(int categoryId)
    {
        Category category = GetById(categoryId);
        _context.Categories.Remove(category);
        _context.SaveChanges();
    }

    public Category Rename(Category category)
    {
        Category oldCategory = GetById(category.Id);
        oldCategory.Name = category.Name;
        Update(oldCategory);
        return oldCategory;
    }
}
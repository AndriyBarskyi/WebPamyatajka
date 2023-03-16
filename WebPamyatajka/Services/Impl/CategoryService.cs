using WebPamyatajka.Data;
using WebPamyatajka.Models;

namespace WebPamyatajka.Services.Impl;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Category> GetAll()
    {
        return _context.Categories.ToList();
    }

    public Category GetById(int id)
    {
        Category category = _context.Categories.First(c => c.Id == id);
        CheckIfCategoryExists(category);
        return category;
    }

    private static void CheckIfCategoryExists(Category category)
    {
        if (category == null)
        {
            throw new ArgumentException(nameof(category));
        }
    }

    public Category Create(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        if (_context.Categories.Find() == category)
        {
            throw new ArgumentException("Such category already exists:", nameof
            (category));
        }
        
        if (string.IsNullOrEmpty(category.Name))
        {
            throw new ArgumentException("Name of the category is required.");
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
}
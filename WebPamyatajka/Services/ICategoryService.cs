using WebPamyatajka.Models;

namespace WebPamyatajka.Services;

public interface ICategoryService
{
    List<Category> GetAll();
    Category GetById(int id);
    Category Create(Category category);
    void Update(Category category);
    void DeleteById(int categoryId);
}
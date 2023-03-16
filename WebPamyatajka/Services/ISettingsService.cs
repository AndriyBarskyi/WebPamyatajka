using WebPamyatajka.Models;

namespace WebPamyatajka.Services;

public interface ISettingsService
{
    List<Settings> GetAll();
    Settings GetById(int id);
    Settings Create(Settings settings);
    void Update(Settings settings);
    void Delete(Settings settings);
}
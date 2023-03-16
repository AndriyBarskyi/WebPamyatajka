using WebPamyatajka.Models;

namespace WebPamyatajka.Services;

public interface ISettingsService
{
    Settings GetByUserId(int userId);
    Settings Create(Settings settings);
    void Update(Settings settings);
    void DeleteById(int settingsId);
}
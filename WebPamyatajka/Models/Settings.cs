namespace WebPamyatajka.Models;

public class Settings
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Theme { get; set; }
    public string Language { get; set; }
    public int CardsPerDay { get; set; }
    public DateTime ReminderTime { get; set; }
}

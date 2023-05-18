using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebPamyatajka.Models;

public class Settings
{
    public string Id { get; set; }
    public IdentityUser User { get; set; }
    [DefaultValue(false)]
    public bool IsDarkTheme { get; set; }
    [Required]
    [StringLength(30)]
    public string Language { get; set; }
    [Range(3, 10)]
    [Required]
    public int CardsPerDay { get; set; }
    [Required]
    public DateTime ReminderTime { get; set; }
}

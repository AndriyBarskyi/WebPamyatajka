using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPamyatajka.Models;

public class StudyLog
{
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int CardId { get; set; }
    [Required]
    public DateTime LastViewedAt { get; set; }
    [Required]
    public DateTime NextViewAt { get; set; }
    [DefaultValue(false)]
    public bool IsKnown { get; set; }
}

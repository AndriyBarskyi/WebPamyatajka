using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebPamyatajka.Models;

public class StudyLog
{
    public int Id { get; set; }
    [ForeignKey("UserId")]
    public string UserId { get; set; }
    [ForeignKey("CardId")]
    public int CardId { get; set; }

    public Card Card { get; set; }

    [Required]
    public DateTime LastViewedAt { get; set; }
    [Required]
    public DateTime NextViewAt { get; set; }
    [DefaultValue(false)]
    public bool IsKnown { get; set; }
    [DefaultValue(false)]
    public bool IsLearnt { get; set; }
}

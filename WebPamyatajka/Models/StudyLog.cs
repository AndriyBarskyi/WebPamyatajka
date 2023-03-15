namespace WebPamyatajka.Models;

public class StudyLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CardId { get; set; }
    public DateTime LastViewedAt { get; set; }
    public DateTime NextViewAt { get; set; }
    public bool IsKnown { get; set; }
}

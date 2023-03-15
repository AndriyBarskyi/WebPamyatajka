namespace WebPamyatajka.Models;

public class Card
{
    public int Id { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

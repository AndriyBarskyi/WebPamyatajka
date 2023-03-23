using System.ComponentModel.DataAnnotations;

namespace WebPamyatajka.Models;

public class Card
{
    public int Id { get; set; }
    [Required]
    [StringLength(60)]
    public string Front { get; set; }
    [Required]
    [StringLength(120)]
    public string Back { get; set; }
    [Required]
    public Category Category { get; set; }
}

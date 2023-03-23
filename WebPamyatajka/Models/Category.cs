using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebPamyatajka.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    [StringLength(40)]
    public string Name { get; set; }
    [DefaultValue(true)]
    public bool IsDefault { get; set; }
    [Required]
    public IdentityUser Creator { get; set; }
}
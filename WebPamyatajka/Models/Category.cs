using Microsoft.AspNetCore.Identity;

namespace WebPamyatajka.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public IdentityUser Creator { get; set; }
}
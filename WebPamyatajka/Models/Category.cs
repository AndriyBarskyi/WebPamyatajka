using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebPamyatajka.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    [StringLength(40)]
    public string Name { get; set; }
    [DefaultValue(false)]
    public bool IsDefault { get; set; }
    [Required] 
    public string CreatorId;
    [ForeignKey("CreatorId")]
    public IdentityUser Creator { get; set; }
}
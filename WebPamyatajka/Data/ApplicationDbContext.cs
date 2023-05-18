using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebPamyatajka.Models;

namespace WebPamyatajka.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<StudyLog> StudyLogs { get; set; }
    public DbSet<Card> Cards { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    /*protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Card>()
            .HasMany(c => c.StudyLogs)
            .WithOne(s => s.Card)
            .OnDelete(DeleteBehavior.Cascade);
    }*/
}
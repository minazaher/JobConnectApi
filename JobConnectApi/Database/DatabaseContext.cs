using JobConnectApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace JobConnectApi.Database;

public class DatabaseContext : IdentityDbContext<IdentityUser>
{
    
    public DbSet<Job> Jobs { get; set; }
    // public DbSet<Employer> Employers { get; set; }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=JobConnectApi.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
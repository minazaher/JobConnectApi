using JobConnectApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobConnectApi.Database;

public class DatabaseContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Employer> Employers { get; set; }
    public DbSet<JobSeeker> JobSeekers { get; set; }
    public DbSet<Proposal> Proposals { get; set; }

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

        // One-To-Many relation states that each Job must be posted by an Employer referenced by their EmployerId.
        modelBuilder.Entity<Job>()
            .HasOne(job => job.Employer) // Job has one associated Employer
            .WithMany(e=> e.PostedPosts) // Employer can have many associated Jobs
            .HasForeignKey(job => job.EmployerId); // Foreign key linking Job to Employer

        // Many-To-One relation: Each Proposal is associated with a Job.
        modelBuilder.Entity<Proposal>()
            .HasOne(p => p.Job) // Proposal has one associated Job
            .WithMany(j=> j.Proposals) // Job can have many associated Proposals
            .HasForeignKey(p => p.JobId); // Foreign key linking Proposal to Job

        // Many-To-One relation: Each Proposal is associated with a JobSeeker.
        modelBuilder.Entity<Proposal>()
            .HasOne(p => p.JobSeeker) // Proposal has one associated JobSeeker
            .WithMany(js=> js.Proposals) // JobSeeker can have many associated Proposals
            .HasForeignKey(p => p.JobSeekerId); // Foreign key linking Proposal to JobSeeker

        modelBuilder.Entity<Job>()
            .HasMany(j => j.Applicants)
            .WithMany(js=> js.AppliedJobs)
            .UsingEntity(join=> join.ToTable("JobApplications"));
        
        
        modelBuilder.Entity<Job>()
            .HasMany(j => j.SavedBy)
            .WithMany(js=> js.SavedJobs)
            .UsingEntity(join=> join.ToTable("SavedJobs"));

        
        modelBuilder.Entity<Job>()
            .HasOne(j => j.Admin)
            .WithMany(j => j.AcceptedJobs)
            .HasForeignKey(j => j.AdminId);


    }
}
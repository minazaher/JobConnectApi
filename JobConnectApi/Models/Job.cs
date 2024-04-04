using Microsoft.EntityFrameworkCore;

namespace JobConnectApi.Models;

public class Job
{
    public int JobId { get; set; }
    public int EmployerId { get; set; }
    public string JobTitle { get; set; }
    public string JobType { get; set; }
    public decimal? JobBudget { get; set; } // Budget can be nullable
    public DateTime PostDate { get; set; }
    public string JobDescription { get; set; }
    public bool IsActive { get; set; } = true;
    public int NumProposals { get; set; }
    
    // Foreign key
    public User Employer { get; set; } // Navigation property referencing Users(UserId)

    // Navigation properties for relationships
    public ICollection<Proposal> Proposals { get; set; } // One-to-Many with Proposals
    public ICollection<SavedJob> SavedJobs { get; set; } // Many-to-Many with Users (through SavedJobs)
    
}

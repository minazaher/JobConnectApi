using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobConnectApi.Models;

[PrimaryKey("JobId")]
public class Job
{
    [Column]
    public int JobId { get; set; }
    [ForeignKey("EmployerId")]
    [Column]
    public string EmployerId { get; set; } // fk referencing the employer that posted the job, (One to One relation)
    [Column]
    public string JobTitle { get; set; }
    [Column]
    public string JobType { get; set; } // Part-time, Full-Time, Contract
    [Column]
    public decimal? Salray { get; set; } // Budget can be nullable
    [Column]
    public DateTime PostDate { get; set; }
    [Column]
    public string JobDescription { get; set; }
    [Column]
    public JobStatus Status { get; set; } = JobStatus.Accepted; // Pending - Accepted
    [ForeignKey("AdminId")]
    [Column]
    public string AcceptedBy { get; set; }  // fk referencing the admin that accepted the job post, (One to One relation)
    [Column]
    public bool IsActive { get; set; }
    
    // This allows easier access to user information
    public virtual IdentityUser Employer { get; set; }
    public virtual IdentityUser Admin { get; set; }  
}


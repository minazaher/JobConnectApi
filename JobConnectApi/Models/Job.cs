using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobConnectApi.Models;

[PrimaryKey("JobId")]
public class Job
{
    [Column]
    public string JobId { get; set; }
    [ForeignKey("EmployerId")]
    [Column]
    public string? EmployerId { get; set; }  // fk referencing the employer that posted the job, (One to One relation)
    [Column]
    public string JobTitle { get; set; }
    [Column]
    public string JobType { get; set; } // Part-time, Full-Time, Contract
    [Column]
    public decimal Salray { get; set; } // Budget can be nullable
    [Column]
    public DateTime PostDate { get; set; }
    [Column]
    public string JobDescription { get; set; }
    [Column]
    public JobStatus Status { get; set; } = JobStatus.Pending; // Pending - Accepted
    [ForeignKey("AdminId")]
    [Column]
    public string? AdminId { get; set; }  // fk referencing the admin that accepted the job post, (One to One relation)
    [Column]
    public bool IsActive { get; set; }
    
    // This allows easier access to user information
    public virtual Employer? Employer { get; set; }
    public virtual Admin? Admin { get; set; }  
    
    // List of JobSeekers That applied to this job
    public virtual List<JobSeeker>? Applicants { get; set; }
    
    // List of JobSeekers That Saved this job
    public virtual List<JobSeeker>? SavedBy { get; set; }
    
    // List of JobSeekers Of Proposals that have been submitted to this job 
    public virtual List<Proposal>? Proposals { get; set; }

}


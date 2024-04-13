using System.ComponentModel.DataAnnotations.Schema;
using JobConnectApi.Models;

namespace JobConnectApi.DTOs;

public class JobRequest
{
    public int JobId { get; set; }
    public string JobTitle { get; set; }
    public string JobType { get; set; } // Part-time, Full-Time, Contract
    public decimal Salray { get; set; } // Budget can be nullable
    public DateTime PostDate { get; set; }
    public string JobDescription { get; set; }
    public JobStatus Status { get; set; } = JobStatus.Pending; // Pending - Accepted
    public string? AcceptedBy { get; set; }  // fk referencing the admin that accepted the job post, (One to One relation)
    public bool IsActive { get; set; } = true;

}
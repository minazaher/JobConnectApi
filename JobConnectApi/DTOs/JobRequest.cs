using System.ComponentModel.DataAnnotations.Schema;
using JobConnectApi.Models;

namespace JobConnectApi.DTOs;

public class JobRequest
{
    public string JobTitle { get; set; }
    public string JobType { get; set; } // Part-time, Full-Time, Contract
    public decimal Salray { get; set; } // Budget can be nullable
    public DateTime PostDate { get; set; }
    public string JobDescription { get; set; }
}
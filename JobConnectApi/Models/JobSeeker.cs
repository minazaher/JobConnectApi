using Microsoft.AspNetCore.Identity;

namespace JobConnectApi.Models;

public class JobSeeker: IdentityUser
{
    
    
    // List of jobs he applied to
    public virtual List<Job>? AppliedJobs { get; set; }
    
    // List of jobs he saved
    public virtual List<Job>? SavedJobs { get; set; }
    
    // List of Job proposals 
    public virtual List<Proposal>? Proposals { get; set; }
}
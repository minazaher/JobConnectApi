using Microsoft.AspNetCore.Identity;

namespace JobConnectApi.Models;


public class Admin: IdentityUser
{
    // Jobs that are accepted by this admin
    public virtual List<Job> AcceptedJobs { get; set; }
}
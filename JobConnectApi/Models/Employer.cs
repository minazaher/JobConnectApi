using Microsoft.AspNetCore.Identity;

namespace JobConnectApi.Models;

public class Employer: IdentityUser
{
    
    public string CompanyName { get; set; }
    public string Industry { get; set; }
    
    // Navigation property for one-to-many relationship with Post
    public virtual List<Job> PostedPosts { get; set; }
    
    
}
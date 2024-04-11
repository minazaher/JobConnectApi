using Microsoft.AspNetCore.Identity;

namespace JobConnectApi.Models;

public class Employer: IdentityUser
{
    
    public required string CompanyName { get; set; }
    public required string Industry { get; set; }
    
    // Navigation property for one-to-many relationship with Post
    public virtual ICollection<Job> PostedPosts { get; set; }
    
    
}
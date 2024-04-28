namespace JobConnectApi.Models;

public class Chat
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EmployerId { get; set; }
    public string JobSeekerId { get; set; }
    public List<Message>? Messages { get; set; }
    
    public Employer? Employer { get; set; }
    public JobSeeker? JobSeeker { get; set; }
}
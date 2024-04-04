namespace JobConnectApi.Models;

public class SavedJob
{
    public int SavedJobId { get; set; }
    public int JobSeekerId { get; set; }
    public int JobId { get; set; }
    
    public Job Job { get; set; }
    public User JobSeeker { get; set; }
    
}
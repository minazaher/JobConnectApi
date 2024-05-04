using JobConnectApi.Models;

namespace JobConnectApi.DTOs;

public class ChatResponseDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EmployerId { get; set; }
    public string JobSeekerId { get; set; }
    public List<Message>? Messages { get; set; }

    public EmployerDto Employer { get; set; }
    public JobSeekerDto JobSeeker { get; set; }
}
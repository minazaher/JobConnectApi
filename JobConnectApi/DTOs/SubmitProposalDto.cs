namespace JobConnectApi.DTOs;

public class SubmitProposalDto
{
    public string JobId { get; set; }
    public string CoverLetter { get; set; }
    public IFormFile Cv { get; set; }
}

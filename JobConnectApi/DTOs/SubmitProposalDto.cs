namespace JobConnectApi.Models;

public class SubmitProposalDto
{
    public int JobId { get; set; }
    public string CoverLetter { get; set; }
    public IFormFile Cv { get; set; }
}

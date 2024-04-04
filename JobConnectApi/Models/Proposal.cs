namespace JobConnectApi.Models;

public class Proposal
{
    public int ProposalId { get; set; }
    public int JobSeekerId { get; set; }
    public int JobId { get; set; }
    public string CoverLetter { get; set; }
    public string AttachmentPath { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string Status { get; set; }
}
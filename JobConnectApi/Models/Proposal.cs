using Microsoft.AspNetCore.Identity;

namespace JobConnectApi.Models;

public class Proposal
{
    public string ProposalId { get; set; }
    public string JobSeekerId { get; set; }
    public int JobId { get; set; }
    public string CoverLetter { get; set; }
    public string AttachmentPath { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string Status { get; set; }

    public virtual Job? Job { get; set; }
    public virtual JobSeeker? JobSeeker { get; set; }
}
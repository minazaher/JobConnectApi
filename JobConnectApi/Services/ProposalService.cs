using JobConnectApi.Controllers.JobSeekerEndpoints;
using JobConnectApi.Database;
using JobConnectApi.Models;

namespace JobConnectApi.Services;

public class ProposalService: IProposalService
{
    private readonly DatabaseContext _databaseContext;

    public ProposalService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Proposal> SaveProposal(SubmitProposalDto proposalDto, String userId)
    {

        var proposal = new Proposal
        {
            ProposalId = Guid.NewGuid().ToString(),
            JobSeekerId = userId,
            JobId = proposalDto.JobId,
            CoverLetter = proposalDto.CoverLetter,
            SubmissionDate = DateTime.Now,
            Status = "Status",
        };

        // Upload CV and save path (replace with your storage logic)
        string cvPath = await UploadCv(proposalDto.Cv);
        proposal.AttachmentPath = cvPath;

        _databaseContext.Proposals.Add(proposal);
        await _databaseContext.SaveChangesAsync();
        return proposal;
    }
    
    private async Task<string> UploadCv(IFormFile cv)
    {
        // Get file name and ensure it has a valid extension
        var fileName = Path.GetFileName(cv.FileName);
        if (!ValidateFileExtension(fileName))
        {
            throw new Exception("Invalid file extension for CV.");
        }

        // Generate a unique file name to avoid conflicts (optional)
        var newFileName = $"{Guid.NewGuid()}_{fileName}";

        // Get the path to the uploads folder (replace with your actual path)
        var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        // Create uploads folder if it doesn't exist
        if (!Directory.Exists(uploadsFolderPath))
        {
            Directory.CreateDirectory(uploadsFolderPath);
        }

        // Save the uploaded file to the uploads folder
        var filePath = Path.Combine(uploadsFolderPath, newFileName);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await cv.CopyToAsync(stream);
        }

        // Return the relative path to the uploaded CV file
        return Path.Combine("uploads", newFileName);
    }

    private bool ValidateFileExtension(string fileName)
    {
        // Implement logic to check for allowed file extensions (e.g., .pdf, .docx)
        // You can use string comparisons or regular expressions
        var allowedExtensions = new string[] { ".pdf", ".docx" };
        return allowedExtensions.Contains(Path.GetExtension(fileName).ToLower());
    }

 
}
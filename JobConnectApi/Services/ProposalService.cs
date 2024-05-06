using JobConnectApi.Controllers.JobSeekerEndpoints;
using JobConnectApi.Database;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobConnectApi.Services;

public class ProposalService(
    IDataRepository<Proposal> dataRepository,
    IChatService chatService,
    IJobService jobService,
    DatabaseContext databaseContext) : IProposalService
{
    public async Task<Proposal> SaveProposal(SubmitProposalDto proposalDto, String userId)
    {
        Console.WriteLine("User id is :" + userId);
        Console.WriteLine("JobId:" + proposalDto.JobId);

        var proposal = new Proposal
        {
            ProposalId = Guid.NewGuid().ToString(),
            JobSeekerId = userId,
            JobId = proposalDto.JobId,
            CoverLetter = proposalDto.CoverLetter,
            SubmissionDate = DateTime.Now,
            Status = ProposalStatus.Pending,
        };

        // Upload CV and save path (replace with your storage logic)
        string cvPath = await UploadCv(proposalDto.Cv);
        proposal.AttachmentPath = cvPath;

        await dataRepository.AddAsync(proposal);
        await dataRepository.Save();
        Console.WriteLine("We reached this point!");
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
        var allowedExtensions = new[] { ".pdf", ".docx" };
        return allowedExtensions.Contains(Path.GetExtension(fileName).ToLower());
    }

    public async Task<List<Proposal>> GetByJobId(string jobId)
    {
        var allProposals = await dataRepository.GetAllAsync();
        var result = allProposals.Where(p => p.JobId.Equals(jobId));
        var response = await databaseContext.Proposals
            .Include(p => p.Job)
            .Include(p => p.JobSeeker)
            .Where(u => u.JobId.Equals(jobId))
            .ToListAsync();
        Console.WriteLine("Reached this point ");

        return response;
    }

    public async Task<Proposal> UpdateProposalStatus(string proposalId, ProposalStatus status, string employerId)
    {
        var proposal = await dataRepository.GetByIdAsync(proposalId);
        proposal.Status = status;
        var saved = await dataRepository.Save();
        if (status == ProposalStatus.Accepted)
        {
            Chat chat = new Chat
            {
                Id = Guid.NewGuid().ToString(),
                JobSeekerId = proposal.JobSeekerId,
                EmployerId = employerId
            };
            saved = saved && await chatService.CreateChat(chat) && await DeActivateJob(proposalId);
        }

        return saved ? proposal : throw new KeyNotFoundException();
    }

    public async Task<Proposal?> GetProposalById(string proposalId)
    {
        var proposal = await databaseContext.Proposals
            .Include(p => p.JobSeeker)
            .Where(u => u.ProposalId.Equals(proposalId))
            .FirstOrDefaultAsync();
     
        return proposal ?? null;
    }

    private async Task<bool> DeActivateJob(string proposalId)
    {
        bool isSaved = false;
        Proposal? proposal = await GetProposalById(proposalId);
        if (proposal != null)
        {
            var jobId = proposal.JobId;
            isSaved = await jobService.DeActivateJob(jobId);
            return isSaved;
        }

        return isSaved;
    }
}
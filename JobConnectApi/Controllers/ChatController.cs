using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers;

[ApiController]
[Authorize(Roles = "JobSeeker, Employer", AuthenticationSchemes = "Bearer")]
[Route("/chat")]
public class ChatController(IChatService chatService) : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> SendMessage(string content, string receiverId,  string chatId)
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            Message message = new Message
            {
                ChatId = chatId,
                Content = content,
                SenderId = userId,
                RecipientId = receiverId,
                SentDate = DateTime.Now,
                MessageId = Guid.NewGuid().ToString()
            };
            var sent = await chatService.SendMessage(message, chatId);
            return sent ? Ok(message) : Problem("Message Not Sent");
        }

        return Unauthorized("You Have to Login First");
    }
}
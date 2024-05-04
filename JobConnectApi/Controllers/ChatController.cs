using JobConnectApi.DTOs;
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
    public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            Message message = new Message
            {
                ChatId = messageDto.ChatId,
                Content = messageDto.Content,
                SenderName = messageDto.SenderName,
                RecipientName = messageDto.RecipientName,
                SentDate = DateTime.Now,
                MessageId = Guid.NewGuid().ToString()
            };
            var sent = await chatService.SendMessage(message, messageDto.ChatId);
            return sent ? Ok(message) : Problem("Message Not Sent");
        }

        return Unauthorized("You Have to Login First");
    }

    [HttpGet("job-seeker")]
    public async Task<IActionResult> GetJobSeekerChats()
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            List<Chat> chats = await chatService.GetChatsByJobSeekerId(userId);
            return Ok(chats);
        }

        return Unauthorized();
    }

    [HttpGet("job-seeker/{chatId}")]
    public async Task<IActionResult> GetJobSeekerChatWithMessages([FromRoute] string chatId)
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId == null) return Unauthorized();
        try
        {
            Chat chat = await chatService.GetJobSeekerChatWithMessages(chatId);
            return Ok(chat);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound("Chat Not Found");
        }
    }
}
using AutoMapper;
using JobConnectApi.DTOs;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobConnectApi.Controllers;

[ApiController]
[Authorize(Roles = "JobSeeker", AuthenticationSchemes = "Bearer")]
[Route("job-seeker/chat")]
public class ChatController(IChatService chatService, IMapper mapper) : ControllerBase
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

    [HttpGet]
    public async Task<IActionResult> GetJobSeekerChats()
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId != null)
        {
            List<Chat> chats = await chatService.GetChatsByJobSeekerId(userId);
            var responseDto = chats.Select(mapper.Map<ChatResponseDto>).ToList();
            return Ok(responseDto);
        }

        return Unauthorized();
    }

    [HttpGet("{chatId}")]
    public async Task<IActionResult> GetJobSeekerChatWithMessages([FromRoute] string chatId)
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        if (userId == null) return Unauthorized();
        try
        {
            Chat chat = await chatService.GetJobSeekerChatWithMessages(chatId);
            var chatResponseDto = mapper.Map<ChatResponseDto>(chat);
            return Ok(chatResponseDto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound("Chat Not Found");
        }
    }
}
using JobConnectApi.Database;
using JobConnectApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JobConnectApi.Services;

public class ChatService(IDataRepository<Chat> chatRepository, IDataRepository<Message> messageRepository, DatabaseContext databaseContext)
    : IChatService
{
    public async Task<bool> CreateChat(Chat chat)
    {
        await chatRepository.AddAsync(chat);
        var isCreated = await chatRepository.Save();
        return isCreated;
    }

    public async Task<bool> SendMessage(Message message, string chatId)
    {
        await messageRepository.AddAsync(message);
        /*
        Chat chat = await GetChatById(chatId);
        if (chat == null)
        {
            throw new KeyNotFoundException("Chat Not Found");
        }
        if (chat.Messages.IsNullOrEmpty())
        {
            chat.Messages = new List<Message>();
        }

        chat.Messages!.Add(message);
        */
        var saved =await messageRepository.Save(); //SaveChat
        return saved;
    }   

    public async Task<List<Message>> GetChatMessages(string chatId)
    {
        Chat chat = await chatRepository.GetByIdAsync(chatId);
        var chatMessages = messageRepository.GetAllAsync().Result.ToList().FindAll(message => message.ChatId == chatId);
        chat.Messages = chatMessages;
        return chatMessages;
    }


    public async Task<bool> DeleteChat(string id)
    {
        Chat chat = await chatRepository.GetByIdAsync(id);
        await chatRepository.DeleteAsync(chat);
        var isRemoved = await chatRepository.Save();
        return isRemoved;
    }

    public async Task<Chat> GetChatById(string id)
    {
        Chat chat = await chatRepository.GetByIdAsync(id);
        chat.Messages = await GetChatMessages(id);
        return chat;
    }

    public async Task<List<Chat>> GetChatsByJobSeekerId(string userId)
    {
        List<Chat> chats =  await databaseContext.Chats
            .Include(c=>c.Messages)
            .Include(c=> c.Employer)
            .Where(c => c.JobSeekerId.Equals(userId)).ToListAsync();
        return chats;
    }
    
    public async Task<List<Chat>> GetChatsByEmployerId(string userId)
    {
        List<Chat> chats =  await databaseContext.Chats
            .Include(c=>c.Messages)
            .Include(c=> c.JobSeeker)
            .Where(c => c.EmployerId.Equals(userId)).ToListAsync();
        return chats;
    }

    public async Task<Chat> GetJobSeekerChatWithMessages(string chatId)
    {
        Chat? chat =  await databaseContext.Chats
            .Include(c=>c.Messages)
            .Include(c=> c.Employer)
            .Include(c=> c.JobSeeker)
            .Where(c => c.Id.Equals(chatId)).FirstOrDefaultAsync();
        return chat ?? throw new KeyNotFoundException("Chat Not Found");
    }
}
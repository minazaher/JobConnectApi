using JobConnectApi.Models;

namespace JobConnectApi.Services;

public interface IChatService
{
    Task<bool> SendMessage(Message message, string chatId);
    Task<List<Message>> GetChatMessages(string chatId);
    Task<bool> CreateChat(Chat chat);
    Task<bool> DeleteChat(string id);
    Task<Chat> GetChatById(string id);
    Task<List<Chat>> GetChatsByJobSeekerId(string userId);
    Task<List<Chat>> GetChatsByEmployerId(string userId);
    public Task<Chat> GetJobSeekerChatWithMessages(string chatId);

}
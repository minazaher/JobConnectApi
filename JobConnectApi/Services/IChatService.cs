using JobConnectApi.Models;

namespace JobConnectApi.Services;

public interface IChatService
{
    void SendMessage(Message message, string chatId);
    Task<List<Message>> GetChatMessages(string chatId);
    
}
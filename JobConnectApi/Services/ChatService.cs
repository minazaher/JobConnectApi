using JobConnectApi.Database;
using JobConnectApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace JobConnectApi.Services;

public class ChatService(IDataRepository<Chat> chatRepository, IDataRepository<Message> messageRepository)
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
        var saved = await chatRepository.Save() &&  await messageRepository.Save();
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
}
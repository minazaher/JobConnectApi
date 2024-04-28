namespace JobConnectApi.Models;

public class Message
{
    public string MessageId { get; set; } = Guid.NewGuid().ToString();
    public string SenderId { get; set; }
    public string RecipientId { get; set; }
    public string ChatId { get; set; }
    
    public string Content { get; set; }
    public DateTime SentDate { get; set; }
    
    public Chat? Chat { get; set; }
}
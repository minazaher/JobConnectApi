using System.Text.Json.Serialization;

namespace JobConnectApi.Models;

public class Message
{
    public string MessageId { get; set; } = Guid.NewGuid().ToString();
    public string SenderName { get; set; }
    public string RecipientName { get; set; }
    public string ChatId { get; set; }
    
    public string Content { get; set; }
    public DateTime SentDate { get; set; }
    
    [JsonIgnore]
    public Chat? Chat { get; set; }
}
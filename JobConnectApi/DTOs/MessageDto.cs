namespace JobConnectApi.DTOs;

public class MessageDto
{
    public string SenderName { get; set; }
    public string RecipientName { get; set; }
    public string ChatId { get; set; }
    
    public string Content { get; set; }
}
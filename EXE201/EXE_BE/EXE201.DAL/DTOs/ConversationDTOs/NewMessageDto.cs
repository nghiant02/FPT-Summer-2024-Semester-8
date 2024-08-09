namespace EXE201.DAL.DTOs.ConversationDTOs;

public class NewMessageDto
{
    public int ConversationId { get; set; }
    public int SenderId { get; set; }
    public string Message1 { get; set; }
    public string Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Seen { get; set; }
}
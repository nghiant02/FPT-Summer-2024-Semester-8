using EXE201.DAL.Models;

namespace EXE201.DAL.DTOs.ConversationDTOs;

public class ViewConversationDto
{
    public int ConversationId { get; set; }
    public int User1Id { get; set; }
    public int User2Id { get; set; }
    public string LastMessage { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public IEnumerable<ViewMessageDto> Messages { get; set; }
}
namespace EXE201.DAL.DTOs.ConversationDTOs;

public class UnseenMessageDto
{
    public int ConversationId { get; set; }
    public IEnumerable<ViewMessageDto> UnseenMessages { get; set; }
}
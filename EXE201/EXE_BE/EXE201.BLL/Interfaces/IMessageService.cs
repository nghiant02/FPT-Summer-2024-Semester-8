using EXE201.DAL.DTOs.ConversationDTOs;
using EXE201.DAL.Models;

namespace EXE201.BLL.Interfaces;

public interface IMessageService
{
    Task<IEnumerable<ViewMessageDto>> GetMessages(int conversationId);
    Task<IEnumerable<UnseenMessageDto>> GetUnseenMessages(int userId);
    Task<ViewConversationDto> NewMessage(NewMessageDto newMessage);
    Task MarkMessagesAsSeen(int conversationId, int userId);
}
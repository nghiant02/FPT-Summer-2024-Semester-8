using EXE201.DAL.DTOs.ConversationDTOs;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Interface;

namespace EXE201.DAL.Interfaces;

public interface IMessageRepository : IGenericRepository<Message>
{
    Task<ViewConversationDto> NewMessageAsync(NewMessageDto newMessage);
    Task<IEnumerable<ViewMessageDto>> GetMessageAsync(int conversationId);
    Task<IEnumerable<UnseenMessageDto>> GetUnseenMessagesAsync(int userId);
    Task MarkMessagesAsSeenAsync(int conversationId, int userId);
}
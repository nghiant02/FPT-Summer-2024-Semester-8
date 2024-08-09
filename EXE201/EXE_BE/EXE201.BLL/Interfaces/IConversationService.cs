using EXE201.DAL.DTOs.ConversationDTOs;
using EXE201.DAL.Models;

namespace EXE201.BLL.Interfaces;

public interface IConversationService
{
    Task<ViewConversationDto> GetConversationByConversationId(int conversationId);
    Task<IEnumerable<ViewConversationDto>> GetConversations(int userId);
    Task<Conversation> FindConversationAsync(int senderId, int receiverId);
    Task<(IEnumerable<ViewConversationDto> conversations, Conversation conversation)> NewConversationAsync(int senderId,
        int receiverId);
}
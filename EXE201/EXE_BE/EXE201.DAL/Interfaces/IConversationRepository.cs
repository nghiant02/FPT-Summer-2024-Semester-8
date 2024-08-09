using EXE201.DAL.DTOs.ConversationDTOs;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Interface;

namespace EXE201.DAL.Interfaces;

public interface IConversationRepository : IGenericRepository<Conversation>
{
    Task<ViewConversationDto> GetConversationByIdAsync(int conversationId);
    Task<IEnumerable<ViewConversationDto>> GetConversations(int userId);
    Task<Conversation> FindConversationAsync(int senderId, int receiverId);

    Task<(IEnumerable<ViewConversationDto> conversations, Conversation conversation)> NewConversationAsync(int senderId,
        int receiverId);
}
using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs.ConversationDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;

namespace EXE201.BLL.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<IEnumerable<ViewMessageDto>> GetMessages(int conversationId)
    {
        return await _messageRepository.GetMessageAsync(conversationId);
    }

    public async Task<IEnumerable<UnseenMessageDto>> GetUnseenMessages(int userId)
    {
        return await _messageRepository.GetUnseenMessagesAsync(userId);
    }

    public async Task<ViewConversationDto> NewMessage(NewMessageDto newMessage)
    {
        return await _messageRepository.NewMessageAsync(newMessage);
    }

    public async Task MarkMessagesAsSeen(int conversationId, int userId)
    {
        await _messageRepository.MarkMessagesAsSeenAsync(conversationId, userId);
    }
}
using EXE201.DAL.DTOs.ConversationDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Implements;
using Microsoft.EntityFrameworkCore;

namespace EXE201.DAL.Repository;

public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    private readonly IConversationRepository _conversationRepository;

    public MessageRepository(EXE201Context context) : base(context)
    {
        _conversationRepository = new ConversationRepository(context);
    }

    public async Task<ViewConversationDto> NewMessageAsync(NewMessageDto newMessage)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var message = new Message
            {
                ConversationId = newMessage.ConversationId,
                Message1 = newMessage.Message1,
                SenderId = newMessage.SenderId,
                Seen = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
    
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
    
            var conversation = await _context.Conversations.FindAsync(newMessage.ConversationId);
            if (conversation != null)
            {
                conversation.LastMessage = GetLastMessagePreview(newMessage.Type, newMessage.Message1);
                conversation.UpdatedAt = DateTime.UtcNow;
                _context.Conversations.Update(conversation);
                await _context.SaveChangesAsync();
            }
    
            await MarkMessagesAsSeenAsync(newMessage.ConversationId, newMessage.SenderId);
    
            var conversations = await _conversationRepository.GetConversationByIdAsync(newMessage.ConversationId);
    
            await transaction.CommitAsync();
    
            return new ViewConversationDto
            {
                ConversationId = conversations.ConversationId,
                LastMessage = conversations.LastMessage,
                User1Id = conversations.User1Id,
                User2Id = conversations.User2Id,
                CreatedAt = conversations.CreatedAt,
                UpdatedAt = conversations.UpdatedAt,
                Messages = conversations.Messages
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.Error.WriteLine("Error new message", ex);
            throw;
        }
    }

    public async Task<IEnumerable<ViewMessageDto>> GetMessageAsync(int conversationId)
    {
        try
        {
            return await _context.Messages
                .Where(m => m.ConversationId == conversationId)
                .Select(m => new ViewMessageDto
                {
                    MessageId = m.MessageId,
                    Message1 = m.Message1,
                    CreatedAt = m.CreatedAt,
                    UpdatedAt = m.UpdatedAt,
                    Seen = m.Seen,
                    SenderId = m.SenderId
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error fetching messages", ex);
            throw;
        }
    }

    public async Task<IEnumerable<UnseenMessageDto>> GetUnseenMessagesAsync(int userId)
    {
        try
        {
            var userConversations = await _conversationRepository.GetConversations(userId);
            var unseenMessagesResults = new List<UnseenMessageDto>();

            foreach (var conversation in userConversations)
            {
                var unseenMessages = await _context.Messages
                    .Where(m => m.ConversationId == conversation.ConversationId && m.SenderId != userId &&
                                !m.Seen.Value)
                    .Select(m => new ViewMessageDto
                    {
                        MessageId = m.MessageId,
                        Message1 = m.Message1,
                        CreatedAt = m.CreatedAt,
                        UpdatedAt = m.UpdatedAt,
                        Seen = m.Seen,
                        SenderId = m.SenderId
                    })
                    .ToListAsync();

                if (unseenMessages.Any())
                {
                    unseenMessagesResults.Add(new UnseenMessageDto()
                    {
                        ConversationId = conversation.ConversationId,
                        UnseenMessages = unseenMessages
                    });
                }
            }

            return unseenMessagesResults;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error retrieving unseen messages", ex);
            throw;
        }
    }

    public async Task MarkMessagesAsSeenAsync(int conversationId, int userId)
    {
        try
        {
            var unseenMessages = await _context.Messages
                .Where(m => m.ConversationId == conversationId && m.SenderId != userId && !m.Seen.Value)
                .ToListAsync();

            unseenMessages.ForEach(m => m.Seen = true);

            _context.Messages.UpdateRange(unseenMessages);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error updating messages", ex);
            throw;
        }
    }

    private string GetLastMessagePreview(string type, string message)
    {
        return type switch
        {
            "text" => message,
            "link" => "link",
            "image" => "image",
            _ => string.Empty,
        };
    }
}
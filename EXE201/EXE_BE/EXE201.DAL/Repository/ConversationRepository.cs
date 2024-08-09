using EXE201.DAL.DTOs.ConversationDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using MCC.DAL.Repository.Implements;
using Microsoft.EntityFrameworkCore;

namespace EXE201.DAL.Repository;

public class ConversationRepository : GenericRepository<Conversation>, IConversationRepository
{
    public ConversationRepository(EXE201Context context) : base(context)
    {
    }

    public async Task<(IEnumerable<ViewConversationDto> conversations, Conversation conversation)> NewConversationAsync(
        int senderId,
        int receiverId)
    {
        try
        {
            var existingConversation = await FindConversationAsync(senderId, receiverId);

            if (existingConversation != null)
            {
                var conversations = await GetConversations(senderId);
                return (conversations, existingConversation);
            }

            var newConversation = new Conversation
            {
                User1Id = senderId,
                User2Id = receiverId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _context.Conversations.AddAsync(newConversation);
            await _context.SaveChangesAsync();

            var newConversations = await GetConversations(senderId);

            return (newConversations, newConversation);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error creating new conversation", ex);
            throw;
        }
    }

    public async Task<Conversation> FindConversationAsync(int senderId, int receiverId)
    {
        try
        {
            return await _context.Conversations
                .FirstOrDefaultAsync(c =>
                    (c.User1Id == senderId && c.User2Id == receiverId) ||
                    (c.User1Id == receiverId && c.User2Id == senderId));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error finding conversation", ex);
            throw;
        }
    }

    public async Task<IEnumerable<ViewConversationDto>> GetConversations(int userId)
    {
        try
        {
            return await _context.Conversations
                .Include(x => x.Messages)
                .Where(x => x.User1Id == userId || x.User2Id == userId)
                .Select(x => new ViewConversationDto
                {
                    ConversationId = x.ConversationId,
                    User1Id = x.User1Id,
                    User2Id = x.User2Id,
                    LastMessage = x.LastMessage,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Messages = x.Messages.Select(y => new ViewMessageDto
                    {
                        MessageId = y.MessageId,
                        Message1 = y.Message1,
                        CreatedAt = y.CreatedAt,
                        UpdatedAt = y.UpdatedAt,
                        Seen = y.Seen,
                        SenderId = y.SenderId
                    }).ToList()
                })
                .OrderByDescending(x => x.UpdatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error getting conversations", ex);
            throw;
        }
    }

    public async Task<ViewConversationDto> GetConversationByIdAsync(int conversationId)
    {
        try
        {
            var conversationDto = await _context.Conversations
                .Include(c => c.Messages)
                .Select(c => new ViewConversationDto
                {
                    ConversationId = c.ConversationId,
                    User1Id = c.User1Id,
                    User2Id = c.User2Id,
                    LastMessage = c.LastMessage,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Messages = c.Messages.Select(y => new ViewMessageDto
                    {
                        MessageId = y.MessageId,
                        Message1 = y.Message1,
                        CreatedAt = y.CreatedAt,
                        UpdatedAt = y.UpdatedAt,
                        Seen = y.Seen,
                        SenderId = y.SenderId
                    }).ToList()
                })
                .FirstOrDefaultAsync(c => c.ConversationId == conversationId);
            return conversationDto;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error getting conversation by ID", ex);
            throw;
        }
    }
}
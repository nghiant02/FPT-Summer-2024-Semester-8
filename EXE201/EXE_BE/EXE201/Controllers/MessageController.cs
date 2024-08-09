using EXE201.BLL.Interfaces;
using EXE201.DAL.DTOs.ConversationDTOs;
using Microsoft.AspNetCore.Mvc;

namespace EXE201.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet("GetMessageByConversationId")]
    public async Task<IActionResult> GetMessageByConversationId(int conversationId)
    {
        var result = await _messageService.GetMessages(conversationId);
        return Ok(result);
    }

    [HttpGet("GetUnseenMessageByUserId")]
    public async Task<IActionResult> GetUnseenMessage(int userId)
    {
        var result = await _messageService.GetUnseenMessages(userId);
        return Ok(result);
    }

    [HttpGet("MarkMessageAsSeen")]
    public async Task<IActionResult> MarkMessageAsSeen(int conversationId, int userId)
    {
        await _messageService.MarkMessagesAsSeen(conversationId, userId);
        return Ok();
    }

    [HttpPost("AddNewMessage")]
    public async Task<IActionResult> NewMessage(NewMessageDto newMessageDto)
    {
        var result = await _messageService.NewMessage(newMessageDto);
        return Ok(result);
    }
}
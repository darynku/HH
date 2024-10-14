using HH.Application.Chat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace HH.Application.Chat.Hubs;

public interface IChatService
{
}
public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;
    private readonly IDistributedCache _cache;

    public ChatHub(IDistributedCache cache,ILogger<ChatHub> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task SendMessage(string userName, string message)
    {
        var messageModel = new Message(userName, message, DateTime.UtcNow);

        try
        {
            await Clients.All.SendAsync("ReceiveMessage", messageModel);
        }
        catch (HubException ex)
        {
            _logger.LogError($"Error while sending message with exteption {ex.Message}");
            throw;
        }
    }

    public async Task JoinChat(string chatName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatName);
        await Clients.All.SendAsync("ReceiveMessage", "joinChat");
    }
    public async Task SendPrivateMessage(string connectionId, string message)
    {
        await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
    }
}

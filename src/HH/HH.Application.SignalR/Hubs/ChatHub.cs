using HH.Application.Chat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

namespace HH.Application.Chat.Hubs;

public interface IChatService
{
    
}
public class ChatHub : Hub
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDistributedCache _cache;

    public ChatHub(IHttpContextAccessor httpContextAccessor, IDistributedCache cache)
    {
        _httpContextAccessor = httpContextAccessor;
        _cache = cache;
    }

    public async Task SendMessage(string message, CancellationToken cancellationToken = default)
    {
        var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
        var messageModel = new Message(userName, message, DateTime.UtcNow);

        await Clients.All.SendAsync("ReceiveMessage", messageModel, cancellationToken);
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

    protected override async void Dispose(bool disposing)
    {
    }
}

using Microsoft.AspNetCore.SignalR;

namespace PhotoSharingApplication.Web.Chat;

public class ChatHub : Hub {
    public async Task SendMessage(string user, string message, int groupId) => 
        await Clients.Group($"photoId-{groupId}").SendAsync("ReceiveMessage", user, message);
    public async Task JoinGroup(int groupId) => 
        await Groups.AddToGroupAsync(Context.ConnectionId, $"photoId-{groupId}");
}

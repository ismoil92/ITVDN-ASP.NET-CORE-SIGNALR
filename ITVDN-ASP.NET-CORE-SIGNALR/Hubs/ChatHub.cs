using Microsoft.AspNetCore.SignalR;

namespace ITVDN_ASP.NET_CORE_SIGNALR.Hubs;

public class ChatHub : Hub
{
    #region METHODS
    /// <summary>
    /// Метод, для входа в чат группу
    /// </summary>
    /// <param name="username">логин пользователя</param>
    /// <param name="groupname">имя группы</param>
    /// <returns>Возвращает объект типа Task</returns>
    public async Task EnterToGroup(string username, string groupname)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
        await Clients.All.SendAsync("Notify", $"Me {username} enter to chat group {groupname}");
    }

    /// <summary>
    /// Метод, для выхода из чат группы
    /// </summary>
    /// <param name="username">логин пользователя</param>
    /// <param name="groupname">имя группы</param>
    /// <returns>Возвращает объект типа Task</returns>
    public async Task ExitFromGroup(string username, string groupname)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupname);
        await Clients.All.SendAsync("Notify", $"{username} exit from chat group {groupname}");
    }

    /// <summary>
    /// Метод, для отправки сообщение всем кто подключен в определенную группу
    /// </summary>
    /// <param name="message">сообщение</param>
    /// <param name="username">логин пользователя</param>
    /// <param name="groupname">имя группы</param>
    /// <returns>Возвращает объект типа Task</returns>
    public async Task SendMessage(string message, string username, string groupname) =>
        await Clients.Group(groupname).SendAsync("Receive", message, username);

    /// <summary>
    /// Метод, для отправки сообщение всем кто подключен в определенную группу кроме отправителя
    /// </summary>
    /// <param name="message">сообщений</param>
    /// <param name="groupname">имя группы</param>
    /// <returns>Возвращает объект типа Task</returns>
    public async Task SendExcept(string message, string groupname) =>
        await Clients.GroupExcept(groupname, Context.ConnectionId).SendAsync("ReceiveMessage", message);
}
#endregion
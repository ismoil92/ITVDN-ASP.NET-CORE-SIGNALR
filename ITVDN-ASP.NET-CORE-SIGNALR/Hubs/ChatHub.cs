using Microsoft.AspNetCore.SignalR;

namespace ITVDN_ASP.NET_CORE_SIGNALR.Hubs;

public class ChatHub : Hub<IChatClient>
{
    #region FIELD
    private readonly ILogger<ChatHub> _logger;
    #endregion

    #region CONSTRUCTOR
    public ChatHub(ILogger<ChatHub> logger) => this._logger = logger;
    #endregion


    #region METHODS

    /// <summary>
    /// Метод, для отправки сообщение всем поключенным клиентам
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <param name="message">сообщения пользователя</param>
    /// <returns>Возвращает объект типа Task</returns>
    public async Task SendMessage(string username, string message)
    {
        _logger.LogInformation($"Message from {username}: {message}");

        await Clients.All.ReceiveMessage(username, message);
    }

    /// <summary>
    /// Переопределенный метод, уведомляет, о подключение нового клиента
    /// </summary>
    /// <returns>Возвращает объект типа Task</returns>
    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation($"Client Connected: {Context.ConnectionId}");
        await Clients.All.UserConnected(Context.ConnectionId);

        await base.OnConnectedAsync();
    }

    /// <summary>
    /// Переопределенный метод, уведомляет, о отключение клиента
    /// </summary>
    /// <param name="exception">объект типа Exception, описывает почему призошло отключение клиента</param>
    /// <returns>Возвращает объект типа Task</returns>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation($"Client Disconnected: {Context.ConnectionId}");
        await Clients.All.UserDisconnected(Context.ConnectionId);

        await base.OnDisconnectedAsync(exception);
    }
    #endregion
}
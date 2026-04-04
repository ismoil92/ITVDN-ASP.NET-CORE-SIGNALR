using Microsoft.AspNetCore.SignalR;

namespace ITVDN_ASP.NET_CORE_SIGNALR.Hubs;

public class ChatHub : Hub
{
    /// <summary>
    /// Метод, для отправки сообщений в реальном времени.
    /// </summary>
    /// <param name="message">сообщений</param>
    /// <param name="username">имя пользователя</param>
    /// <returns>Возвращает объект типа Task</returns>
    public async Task Send(string message, string username)
    {
        message += $".Курс валюты в Узбекистане:{Random.Shared.Next(12100, 12201)}";
        await Clients.All.SendAsync("Receive", message , username);
    }
}
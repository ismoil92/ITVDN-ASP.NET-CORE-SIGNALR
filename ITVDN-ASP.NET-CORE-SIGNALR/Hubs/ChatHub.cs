using Microsoft.AspNetCore.SignalR;

namespace ITVDN_ASP.NET_CORE_SIGNALR.Hubs;

public class ChatHub : Hub
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task Send(string message, string username) =>
        await Clients.All.SendAsync("Receive", message +$". Курс валюты в Узбекистане: { Random.Shared.Next(12100, 12201)}", username);
}
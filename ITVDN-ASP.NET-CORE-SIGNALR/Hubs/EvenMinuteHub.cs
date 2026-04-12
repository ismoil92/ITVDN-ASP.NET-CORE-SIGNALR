using Microsoft.AspNetCore.SignalR;

namespace ITVDN_ASP.NET_CORE_SIGNALR.Hubs;

public class EvenMinuteHub : Hub
{
    #region METHODS
    /// <summary>
    /// Метод, для отправки сообщение всем поключенным клиентам
    /// </summary>
    /// <param name="message">сообщение пользователя</param>
    /// <returns>Возвращает объект типа Task</returns>
    public async Task Send(string message) => await Clients.All.SendAsync("Receive", message);


    /// <summary>
    /// Переопределенный метод, уведомляет, о подключение нового клиента, при чётном минуты времени
    /// </summary>
    /// <returns>Возвращает объект типа Task</returns>
    public override async Task OnConnectedAsync()
    {
        int minute = DateTime.Now.Minute;

        if(minute %2 != 0)
        {
            Console.WriteLine($"Minute is not even:{minute}. Client not connected.");
            Context.Abort();
            return;
        }

        await base.OnConnectedAsync();
    }
    #endregion
}
using ITVDN_ASP.NET_CORE_SIGNALR.Hubs;
using ITVDN_ASP.NET_CORE_SIGNALR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ITVDN_ASP.NET_CORE_SIGNALR.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotifyController : ControllerBase
{
    #region FIELD
    private readonly IHubContext<ChatHub, IChatClient> _hubContext;
    #endregion

    #region CONSTRUCTOR
    public NotifyController(IHubContext<ChatHub, IChatClient> hubContext) => this._hubContext = hubContext;
    #endregion


    #region METHOD

    /// <summary>
    /// Метод, который будет оповещать всех клиентов
    /// </summary>
    /// <param name="person">Объект типа Person</param>
    /// <returns>Возвращает обобщенный тип Task.
    /// Внутри обобщенного метода имеется объект типа IActionResult</returns>
    [HttpPost("broadcast")]
    public async Task<IActionResult> Broadcast([FromBody] Person person)
    {
        await _hubContext.Clients.All.ReceiveMessage(person.Username!, person.Message!);
        return Ok("Notification send");
    }
    #endregion
}
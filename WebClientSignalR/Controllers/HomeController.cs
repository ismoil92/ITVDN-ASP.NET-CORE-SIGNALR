using Microsoft.AspNetCore.Mvc;

namespace WebClientSignalR.Controllers;

public class HomeController : Controller
{
    /// <summary>
    /// Метод, возвращает представление с именем
    /// </summary>
    /// <returns>Возвращает тип объекта IActionResult</returns>
    public IActionResult Index() => View();
}
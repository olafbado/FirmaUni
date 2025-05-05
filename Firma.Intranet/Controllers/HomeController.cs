using Microsoft.AspNetCore.Mvc;

namespace Firma.Intranet.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

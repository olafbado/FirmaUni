using Microsoft.AspNetCore.Mvc;
using Firma.PortalWWW.Models;

[Route("")]
public class HomeController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("sklep")]
    public IActionResult Sklep()
    {
        return View();
    }

    [HttpGet("o-firmie")]
    public IActionResult OFirmie()
    {
        return View();
    }

    [HttpGet("historia")]
    public IActionResult Historia()
    {
        return View();
    }

    [HttpGet("uslugi")]
    public IActionResult Uslugi() => View();

    [HttpGet("polityka-prywatnosci")]
    public IActionResult Privacy()
    {
        return View();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

public class SklepController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
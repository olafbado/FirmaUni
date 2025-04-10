using Microsoft.AspNetCore.Mvc;

public class UslugiController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
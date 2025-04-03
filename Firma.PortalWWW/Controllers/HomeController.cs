using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Firma.PortalWWW.Models;
using Firma.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //to jest pole odpowiedzialne za BD
    private readonly FirmaContext _context; 
    public HomeController(ILogger<HomeController> logger, FirmaContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index(int? id)//jak parametr funkcja dostaje id strony, ktora kliknieto
    {
        //
        //ViewBag.ModelStrony =
        //    (
        //        from strona in _context.Strona//dla ka�dej strony z bazy danych context stron
        //        orderby strona.Pozycja //posortowanej wzgledem pozycji
        //        select strona //pobieramy strone
        //    ).ToList();
        ViewBag.ModelStrony = await _context.Strona.OrderBy(s => s.Pozycja).ToListAsync();
        //
        //ViewBag.ModelAktualnosci=
        //    (
        //        from aktualnosc in _context.Aktualnosc
        //        orderby aktualnosc.Pozycja descending //ostatenie aktualnosci
        //        select aktualnosc
        //    ).Take(4).ToList(); //wybieram tylko 4 ostatnie
        ViewBag.ModelAktualnosci = 
            await _context.Aktualnosc.OrderByDescending(a => a.Pozycja).Take(4).ToListAsync();
        if(id == null)//id jest nullem przy pierwszym uruchomienu i wtedy wyswietlamy strone o id=1
        {
            id = 1;
        }
        //asynchronicznie odnajduje w bazie danych strone o danym id
        var item=await _context.Strona.FindAsync(id);
        //odnalezion� strone o danym id przekazujemy do widoku do wyswietlenia
        if (item == null)
{
    return NotFound(); // lub View("Blad") itp.
}
return View(item);
        return View(item);//jak funkcja nazywa si� index, to strone te� przekazujemy do widoku o nazwie index
    }
    
    public IActionResult OpisFirmy()
    {
        return View();
    }
    public IActionResult HistoriaFirmy()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

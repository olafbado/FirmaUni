using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers;

public class ZamowienieController : Controller
{
    private readonly FirmaContext _context;

    public ZamowienieController(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var zamowienia = await _context.Zamowienie.Include(z => z.Pozycje).ToListAsync();
        return View(zamowienia);
    }
}

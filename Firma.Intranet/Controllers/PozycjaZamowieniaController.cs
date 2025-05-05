using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers;

public class PozycjaZamowieniaController : Controller
{
    private readonly FirmaContext _context;

    public PozycjaZamowieniaController(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var pozycje = await _context
            .PozycjaZamowienia.Include(p => p.Towar)
            .Include(p => p.Zamowienie)
            .ToListAsync();
        return View(pozycje);
    }
}

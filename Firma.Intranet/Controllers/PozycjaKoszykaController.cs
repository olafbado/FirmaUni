using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers;

public class PozycjaKoszykaController : Controller
{
    private readonly FirmaContext _context;

    public PozycjaKoszykaController(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var pozycje = await _context
            .PozycjaKoszyka.Include(p => p.Towar)
            .Include(p => p.Koszyk)
            .ToListAsync();
        return View(pozycje);
    }
}

using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers;

public class KoszykController : Controller
{
    private readonly FirmaContext _context;

    public KoszykController(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var koszyki = await _context.Koszyk.Include(k => k.Pozycje).ToListAsync();
        return View(koszyki);
    }
}

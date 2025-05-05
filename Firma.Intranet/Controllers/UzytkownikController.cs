using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers;

public class UzytkownikController : Controller
{
    private readonly FirmaContext _context;

    public UzytkownikController(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Uzytkownik.ToListAsync());
    }
}

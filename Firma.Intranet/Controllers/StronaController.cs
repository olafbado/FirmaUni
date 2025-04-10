using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers;

public class StronaController : Controller
{
    private readonly FirmaContext _context;

    public StronaController(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Strona.ToListAsync());
    }

}

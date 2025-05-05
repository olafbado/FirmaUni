using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers;

public class TowarController : Controller
{
    private readonly FirmaContext _context;

    public TowarController(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var firmaIntranetContext = _context.Towar.Include(t => t.Rodzaj);
        return View(await firmaIntranetContext.ToListAsync());
    }
}

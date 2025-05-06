using Firma.Data.Data;
using Firma.Data.Data.Sklep;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers;

public class RodzajController : Controller
{
    private readonly FirmaContext _context;

    public RodzajController(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Rodzaj.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Rodzaj rodzaj)
    {
        if (!ModelState.IsValid)
            return View(rodzaj);

        _context.Add(rodzaj);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var rodzaj = await _context.Rodzaj.FindAsync(id);
        if (rodzaj == null)
            return NotFound();

        return View(rodzaj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Rodzaj rodzaj)
    {
        if (id != rodzaj.IdRodzaju)
            return NotFound();

        if (!ModelState.IsValid)
            return View(rodzaj);

        _context.Update(rodzaj);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var rodzaj = await _context.Rodzaj.FindAsync(id);
        if (rodzaj != null)
        {
            _context.Rodzaj.Remove(rodzaj);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var rodzaj = await _context.Rodzaj.FindAsync(id);
        if (rodzaj == null)
            return NotFound();

        return View(rodzaj);
    }
}

using Firma.Data.Data;
using Firma.Data.Data.Sklep;
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
        return View(await _context.Towar.Include(t => t.Rodzaj).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var towar = await _context
            .Towar.Include(t => t.Rodzaj)
            .FirstOrDefaultAsync(t => t.IdTowaru == id);
        if (towar == null)
            return NotFound();
        return View(towar);
    }

    public IActionResult Create()
    {
        ViewBag.Rodzaje = _context.Rodzaj.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Towar towar)
    {
        if (ModelState.IsValid)
        {
            _context.Add(towar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Rodzaje = _context.Rodzaj.ToList();
        return View(towar);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var towar = await _context.Towar.FindAsync(id);
        if (towar == null)
            return NotFound();
        ViewBag.Rodzaje = _context.Rodzaj.ToList();
        return View(towar);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Towar towar)
    {
        if (id != towar.IdTowaru)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(towar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Rodzaje = _context.Rodzaj.ToList();
        return View(towar);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var towar = await _context.Towar.FindAsync(id);
        if (towar != null)
        {
            _context.Towar.Remove(towar);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}

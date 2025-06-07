using Firma.Data.Data;
using Firma.Data.Data.CMS;
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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Strona strona)
    {
        if (ModelState.IsValid)
        {
            _context.Add(strona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(strona);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var strona = await _context.Strona.FindAsync(id);
        if (strona == null)
            return NotFound();

        return View(strona);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Strona strona)
    {
        if (id != strona.IdStrony)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(strona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        return View(strona);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var strona = await _context.Strona.FirstOrDefaultAsync(m => m.IdStrony == id);
        if (strona == null)
            return NotFound();

        return View(strona);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var strona = await _context.Strona.FirstOrDefaultAsync(m => m.IdStrony == id);
        if (strona == null)
            return NotFound();

        return View(strona);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var strona = await _context.Strona.FindAsync(id);
        if (strona != null)
        {
            _context.Strona.Remove(strona);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}

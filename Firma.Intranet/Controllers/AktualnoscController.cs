using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers;

public class AktualnoscController : Controller
{
    private readonly FirmaContext _context;

    public AktualnoscController(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Aktualnosc.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Aktualnosc aktualnosc)
    {
        if (ModelState.IsValid)
        {
            _context.Add(aktualnosc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(aktualnosc);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var aktualnosc = await _context.Aktualnosc.FindAsync(id);
        if (aktualnosc == null)
            return NotFound();
        return View(aktualnosc);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Aktualnosc aktualnosc)
    {
        if (id != aktualnosc.IdAktualnosci)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(aktualnosc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(aktualnosc);
    }

    public async Task<IActionResult> Details(int id)
    {
        var aktualnosc = await _context.Aktualnosc.FindAsync(id);
        if (aktualnosc == null)
            return NotFound();
        return View(aktualnosc);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var aktualnosc = await _context.Aktualnosc.FindAsync(id);
        if (aktualnosc != null)
        {
            _context.Aktualnosc.Remove(aktualnosc);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}

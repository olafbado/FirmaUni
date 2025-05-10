using Firma.Data.Data;
using Firma.Data.Data.Sklep;
using Firma.Intranet.Models;
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
        var koszyki = await _context
            .Koszyk.Include(k => k.Uzytkownik)
            .Include(k => k.Pozycje)
            .ThenInclude(p => p.Towar)
            .ToListAsync();

        return View(koszyki);
    }

    public async Task<IActionResult> Details(int id)
    {
        var koszyk = await _context
            .Koszyk.Include(k => k.Uzytkownik)
            .Include(k => k.Pozycje)
            .ThenInclude(p => p.Towar)
            .FirstOrDefaultAsync(k => k.IdKoszyka == id);

        if (koszyk == null)
            return NotFound();

        return View(koszyk);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var towary = _context.Towar.ToList();
        var model = new KoszykViewModel
        {
            Pozycje = towary
                .Select(t => new PozycjaKoszykaVM
                {
                    TowarId = t.IdTowaru,
                    TowarNazwa = t.Nazwa,
                    CenaJednostkowa = t.Cena,
                    Ilosc = 0,
                })
                .ToList(),
        };

        ViewData["Uzytkownicy"] = _context.Uzytkownik.ToList();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(KoszykViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var koszyk = new Koszyk
        {
            UzytkownikId = vm.UzytkownikId,
            Pozycje = vm
                .Pozycje.Where(p => p.Ilosc > 0)
                .Select(p => new PozycjaKoszyka { TowarId = p.TowarId, Ilosc = p.Ilosc })
                .ToList(),
        };

        _context.Koszyk.Add(koszyk);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var koszyk = await _context
            .Koszyk.Include(k => k.Pozycje)
            .ThenInclude(p => p.Towar)
            .FirstOrDefaultAsync(k => k.IdKoszyka == id);

        if (koszyk == null)
            return NotFound();

        var towary = _context.Towar.ToList();
        var vm = new KoszykViewModel
        {
            IdKoszyka = koszyk.IdKoszyka,
            UzytkownikId = koszyk.UzytkownikId,
            Pozycje = towary
                .Select(t =>
                {
                    var istniejaca = koszyk.Pozycje.FirstOrDefault(p => p.TowarId == t.IdTowaru);
                    return new PozycjaKoszykaVM
                    {
                        TowarId = t.IdTowaru,
                        TowarNazwa = t.Nazwa,
                        CenaJednostkowa = t.Cena,
                        Ilosc = istniejaca?.Ilosc ?? 0,
                    };
                })
                .ToList(),
        };

        ViewData["Uzytkownicy"] = _context.Uzytkownik.ToList();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, KoszykViewModel vm)
    {
        if (id != vm.IdKoszyka)
            return NotFound();

        var koszyk = await _context
            .Koszyk.Include(k => k.Pozycje)
            .FirstOrDefaultAsync(k => k.IdKoszyka == id);

        if (koszyk == null)
            return NotFound();

        koszyk.UzytkownikId = vm.UzytkownikId;
        koszyk.Pozycje.Clear();

        foreach (var p in vm.Pozycje.Where(p => p.Ilosc > 0))
        {
            koszyk.Pozycje.Add(new PozycjaKoszyka { TowarId = p.TowarId, Ilosc = p.Ilosc });
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var koszyk = await _context.Koszyk.FindAsync(id);
        if (koszyk == null)
            return NotFound();

        _context.Koszyk.Remove(koszyk);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

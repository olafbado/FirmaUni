using Firma.Data.Data;
using Firma.Data.Data.Sklep;
using Firma.Intranet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers;

public class ZamowienieController : Controller
{
    private readonly FirmaContext _context;

    public ZamowienieController(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var zamowienia = await _context.Zamowienie.Include(z => z.Uzytkownik).ToListAsync();
        return View(zamowienia);
    }

    public async Task<IActionResult> Details(int id)
    {
        var zamowienie = await _context
            .Zamowienie.Include(z => z.Uzytkownik)
            .FirstOrDefaultAsync(z => z.IdZamowienia == id);

        if (zamowienie == null)
            return NotFound();

        return View(zamowienie);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Uzytkownicy"] = _context.Uzytkownik.ToList();
        ViewData["Koszyki"] = _context.Koszyk.Include(k => k.Uzytkownik).ToList();
        return View(new ZamowienieViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ZamowienieViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Uzytkownicy"] = _context.Uzytkownik.ToList();
            ViewData["Koszyki"] = _context.Koszyk.ToList();
            return View(vm);
        }

        var koszyk = await _context
            .Koszyk.Include(k => k.Pozycje)
            .ThenInclude(p => p.Towar)
            .FirstOrDefaultAsync(k => k.IdKoszyka == vm.KoszykId);

        if (koszyk == null)
        {
            ModelState.AddModelError("KoszykId", "Wybrany koszyk nie istnieje.");
            ViewData["Uzytkownicy"] = _context.Uzytkownik.ToList();
            ViewData["Koszyki"] = _context.Koszyk.ToList();
            return View(vm);
        }

        var suma = koszyk.Pozycje.Sum(p => p.Ilosc * p.Towar.Cena);

        var zamowienie = new Zamowienie
        {
            UzytkownikId = vm.UzytkownikId,
            Adres = vm.Adres,
            MetodaPlatnosci = vm.MetodaPlatnosci,
            KoszykId = vm.KoszykId,
            Suma = suma,
            DataZamowienia = DateTime.UtcNow,
        };

        _context.Zamowienie.Add(zamowienie);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var zamowienie = await _context
            .Zamowienie.Include(z => z.Koszyk)
            .FirstOrDefaultAsync(z => z.IdZamowienia == id);

        if (zamowienie == null)
            return NotFound();

        var vm = new ZamowienieViewModel
        {
            IdZamowienia = zamowienie.IdZamowienia,
            UzytkownikId = zamowienie.UzytkownikId,
            Adres = zamowienie.Adres,
            MetodaPlatnosci = zamowienie.MetodaPlatnosci,
            KoszykId = zamowienie.KoszykId,
        };

        ViewData["Uzytkownicy"] = _context.Uzytkownik.ToList();
        ViewData["Koszyki"] = _context.Koszyk.Include(k => k.Uzytkownik).ToList();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ZamowienieViewModel vm)
    {
        if (id != vm.IdZamowienia)
            return NotFound();

        if (!ModelState.IsValid)
        {
            ViewData["Uzytkownicy"] = _context.Uzytkownik.ToList();
            ViewData["Koszyki"] = _context.Koszyk.ToList();
            return View(vm);
        }

        var zamowienie = await _context.Zamowienie.FirstOrDefaultAsync(z => z.IdZamowienia == id);
        if (zamowienie == null)
            return NotFound();

        var koszyk = await _context
            .Koszyk.Include(k => k.Pozycje)
            .ThenInclude(p => p.Towar)
            .FirstOrDefaultAsync(k => k.IdKoszyka == vm.KoszykId);

        if (koszyk == null)
        {
            ModelState.AddModelError("KoszykId", "Wybrany koszyk nie istnieje.");
            ViewData["Uzytkownicy"] = _context.Uzytkownik.ToList();
            ViewData["Koszyki"] = _context.Koszyk.ToList();
            return View(vm);
        }

        zamowienie.UzytkownikId = vm.UzytkownikId;
        zamowienie.Adres = vm.Adres;
        zamowienie.MetodaPlatnosci = vm.MetodaPlatnosci;
        zamowienie.KoszykId = vm.KoszykId;
        zamowienie.Suma = koszyk.Pozycje.Sum(p => p.Ilosc * p.Towar.Cena);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var zamowienie = await _context.Zamowienie.FindAsync(id);
        if (zamowienie == null)
            return NotFound();

        _context.Zamowienie.Remove(zamowienie);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

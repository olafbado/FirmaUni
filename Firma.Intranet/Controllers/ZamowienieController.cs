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
            .Include(z => z.Pozycje)
            .ThenInclude(p => p.Towar)
            .FirstOrDefaultAsync(z => z.IdZamowienia == id);

        if (zamowienie == null)
            return NotFound();

        return View(zamowienie);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new ZamowienieViewModel
        {
            Pozycje = _context
                .Towar.Select(t => new PozycjaZamowieniaVM
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
    public async Task<IActionResult> Create(ZamowienieViewModel vm)
    {
        // print vm to console
        Console.WriteLine($"UzytkownikId: {vm.UzytkownikId}");
        if (vm.Pozycje == null)
        {
            Console.WriteLine("Pozycje == null");
        }
        else
        {
            foreach (var pozycja in vm.Pozycje)
            {
                if (pozycja == null)
                {
                    Console.WriteLine("Jedna z pozycji to null");
                    continue;
                }

                Console.WriteLine(
                    $"TowarId: {pozycja.TowarId}, Ilosc: {pozycja.Ilosc}, CenaJednostkowa: {pozycja.CenaJednostkowa}"
                );
            }
        }
        if (!ModelState.IsValid)
        {
            ViewData["Uzytkownicy"] = _context.Uzytkownik.ToList();
            return View(vm);
        }

        var pozycje = vm
            .Pozycje.Where(p => p.Ilosc > 0)
            .Select(p => new PozycjaZamowienia
            {
                TowarId = p.TowarId,
                Ilosc = p.Ilosc,
                CenaJednostkowa = p.CenaJednostkowa,
            })
            .ToList();

        var zamowienie = new Zamowienie
        {
            UzytkownikId = vm.UzytkownikId,
            DataZamowienia = DateTime.UtcNow,
            Pozycje = pozycje,
            Suma = pozycje.Sum(p => p.CenaJednostkowa * p.Ilosc),
        };

        _context.Zamowienie.Add(zamowienie);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var zamowienie = await _context
            .Zamowienie.Include(z => z.Pozycje)
            .FirstOrDefaultAsync(z => z.IdZamowienia == id);

        if (zamowienie == null)
            return NotFound();

        var towary = await _context.Towar.ToListAsync();

        var vm = new ZamowienieViewModel
        {
            IdZamowienia = zamowienie.IdZamowienia,
            UzytkownikId = zamowienie.UzytkownikId,
            Pozycje = towary
                .Select(t =>
                {
                    var istniejacaPozycja = zamowienie.Pozycje.FirstOrDefault(p =>
                        p.TowarId == t.IdTowaru
                    );
                    return new PozycjaZamowieniaVM
                    {
                        TowarId = t.IdTowaru,
                        TowarNazwa = t.Nazwa,
                        CenaJednostkowa = t.Cena,
                        Ilosc = istniejacaPozycja?.Ilosc ?? 0,
                    };
                })
                .ToList(),
        };

        ViewData["Uzytkownicy"] = await _context.Uzytkownik.ToListAsync();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ZamowienieViewModel vm)
    {
        Console.WriteLine($"id: {id}");
        Console.WriteLine($"vm.IdZamowienia: {vm.IdZamowienia}");
        if (id != vm.IdZamowienia)
            return NotFound();

        var zamowienie = await _context
            .Zamowienie.Include(z => z.Pozycje)
            .FirstOrDefaultAsync(z => z.IdZamowienia == id);

        if (zamowienie == null)
            return NotFound();

        // Zaktualizuj dane
        zamowienie.UzytkownikId = vm.UzytkownikId;
        zamowienie.Pozycje.Clear();

        foreach (var p in vm.Pozycje.Where(p => p.Ilosc > 0))
        {
            zamowienie.Pozycje.Add(
                new PozycjaZamowienia
                {
                    TowarId = p.TowarId,
                    Ilosc = p.Ilosc,
                    CenaJednostkowa = _context.Towar.Find(p.TowarId)?.Cena ?? 0,
                }
            );
        }

        zamowienie.Suma = zamowienie.Pozycje.Sum(p => p.Ilosc * p.CenaJednostkowa);

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

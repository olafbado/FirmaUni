using Firma.Data.Data;
using Firma.Data.Data.Sklep;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Portal.Controllers;

public class ZamowienieController : Controller
{
    private readonly FirmaContext _context;

    public ZamowienieController(FirmaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Nowe()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Nowe(string adres, string metodaPlatnosci)
    {
        var userId = "1";
        var koszyk = await _context.Koszyk
            .Include(k => k.Pozycje)
            .ThenInclude(p => p.Towar)
            .FirstOrDefaultAsync(k => k.UzytkownikId == userId && !k.CzyZamowiony);

        if (koszyk == null || !koszyk.Pozycje.Any())
        {
            TempData["Error"] = "Koszyk jest pusty.";
            return RedirectToAction("Index", "Koszyk");
        }

        var zamowienie = new Zamowienie
        {
            UzytkownikId = userId,
            Adres = adres,
            MetodaPlatnosci = metodaPlatnosci,
            Suma = koszyk.Pozycje.Sum(p => p.Ilosc * p.Towar!.Cena),
            KoszykId = koszyk.IdKoszyka,
            DataZamowienia = DateTime.UtcNow
        };

        _context.Zamowienie.Add(zamowienie);
        await _context.SaveChangesAsync();

        koszyk.CzyZamowiony = true;

        _context.Koszyk.Update(koszyk);

        foreach (var poz in koszyk.Pozycje)
        {
            var pozycjaZamowienia = new PozycjaZamowienia
            {
                ZamowienieId = zamowienie.IdZamowienia,
                TowarId = poz.TowarId,
                Ilosc = poz.Ilosc,
                CenaJednostkowa = poz.Towar!.Cena
            };
            _context.PozycjaZamowienia.Add(pozycjaZamowienia);
        }

        await _context.SaveChangesAsync();

        TempData["Success"] = "Zamówienie złożone pomyślnie!";
        return RedirectToAction("Sukces");
    }

    public IActionResult Sukces()
    {
        return View();
    }
}

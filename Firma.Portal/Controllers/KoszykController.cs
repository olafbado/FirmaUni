using Firma.Data.Data;
using Firma.Data.Data.Sklep;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Firma.Portal.Controllers;

public class KoszykController : Controller
{
    private readonly FirmaContext _context;

    public KoszykController(FirmaContext context)
    {
        _context = context;
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "1"; // tymczasowo "anonim" jeśli bez auth
    }

    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();
        var koszyk = await _context.Koszyk
            .Include(k => k.Pozycje)
            .ThenInclude(p => p.Towar)
            .FirstOrDefaultAsync(k => k.UzytkownikId == userId && !k.CzyZamowiony);

        return View(koszyk ?? new Koszyk { Pozycje = new List<PozycjaKoszyka>() });
    }

    [HttpPost]
    public async Task<IActionResult> Dodaj(int idTowaru, int ilosc)
    {
        var userId = "1"; // z ClaimsPrincipal

        var koszyk = await _context.Koszyk
            .Include(k => k.Pozycje)
            .FirstOrDefaultAsync(k => k.UzytkownikId == userId && !k.CzyZamowiony);

        // Jeśli nie ma aktywnego koszyka → tworzymy nowy
        if (koszyk == null)
        {
            koszyk = new Koszyk
            {
                UzytkownikId = userId,
                DataUtworzenia = DateTime.UtcNow,
                CzyZamowiony = false
            };
            _context.Koszyk.Add(koszyk);
            await _context.SaveChangesAsync(); // zapis, żeby mieć IdKoszyka
        }

        // Szukamy, czy pozycja już istnieje
        var istniejącaPozycja = koszyk.Pozycje.FirstOrDefault(p => p.TowarId == idTowaru);

        if (istniejącaPozycja != null)
        {
            istniejącaPozycja.Ilosc += ilosc;
            _context.Update(istniejącaPozycja);
        }
        else
        {
            var pozycja = new PozycjaKoszyka
            {
                KoszykId = koszyk.IdKoszyka,
                TowarId = idTowaru,
                Ilosc = ilosc
            };
            _context.PozycjaKoszyka.Add(pozycja);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Towar", new { id = idTowaru });
    }

    [HttpPost]
    public async Task<IActionResult> Usun(int id)
    {
        var pozycja = await _context.PozycjaKoszyka.FindAsync(id);
        if (pozycja != null)
        {
            _context.PozycjaKoszyka.Remove(pozycja);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> ZmienIlosc(int id, int delta)
    {
        var pozycja = await _context.PozycjaKoszyka
            .Include(p => p.Towar)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pozycja == null)
            return RedirectToAction("Index");

        if (delta > 0)
        {
            // Sprawdź, czy nie przekracza stanu magazynowego
            if (pozycja.Towar!.Ilosc >= (pozycja.Ilosc + delta))
            {
                pozycja.Ilosc += delta;
            }
        }
        else if (delta < 0)
        {
            pozycja.Ilosc += delta;
            if (pozycja.Ilosc <= 0)
            {
                _context.PozycjaKoszyka.Remove(pozycja);
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

}

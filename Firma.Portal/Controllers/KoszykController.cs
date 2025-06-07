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
        return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "1";
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
        var userId = "1";

        var towar = await _context.Towar.FindAsync(idTowaru);
        if (towar == null || towar.Ilosc < ilosc)
        {
            TempData["Error"] = "Nie ma wystarczającej ilości towaru.";
            return RedirectToAction("Details", "Towar", new { id = idTowaru });
        }

        var koszyk = await _context.Koszyk
            .Include(k => k.Pozycje)
            .FirstOrDefaultAsync(k => k.UzytkownikId == userId && !k.CzyZamowiony);

        if (koszyk == null)
        {
            koszyk = new Koszyk
            {
                UzytkownikId = userId,
                DataUtworzenia = DateTime.UtcNow,
                CzyZamowiony = false
            };
            _context.Koszyk.Add(koszyk);
            await _context.SaveChangesAsync();
        }

        var istniejącaPozycja = koszyk.Pozycje.FirstOrDefault(p => p.TowarId == idTowaru);

        if (istniejącaPozycja != null)
        {
            istniejącaPozycja.Ilosc += ilosc;
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

        towar.Ilosc -= ilosc;

        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Towar", new { id = idTowaru });
    }

    [HttpPost]
    public async Task<IActionResult> Usun(int id)
    {
        var pozycja = await _context.PozycjaKoszyka
            .Include(p => p.Towar)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pozycja != null)
        {
            pozycja.Towar!.Ilosc += pozycja.Ilosc;

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

        if (pozycja == null || pozycja.Towar == null)
            return RedirectToAction("Index");

        if (delta > 0)
        {
            if (pozycja.Towar.Ilosc >= delta)
            {
                pozycja.Ilosc += delta;
                pozycja.Towar.Ilosc -= delta;
            }
        }
        else if (delta < 0)
        {
            var absDelta = Math.Abs(delta);
            pozycja.Ilosc -= absDelta;
            pozycja.Towar.Ilosc += absDelta;

            if (pozycja.Ilosc <= 0)
            {
                _context.PozycjaKoszyka.Remove(pozycja);
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }


}

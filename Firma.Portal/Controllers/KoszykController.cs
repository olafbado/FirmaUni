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
            .FirstOrDefaultAsync(k => k.UzytkownikId == userId);

        return View(koszyk ?? new Koszyk { Pozycje = new List<PozycjaKoszyka>() });
    }

    [HttpPost]
    public async Task<IActionResult> Dodaj(int idTowaru, int ilosc)
    {
        var userId = GetUserId();

        var koszyk = await _context.Koszyk
            .Include(k => k.Pozycje)
            .FirstOrDefaultAsync(k => k.UzytkownikId == userId);

        if (koszyk == null)
        {
            koszyk = new Koszyk
            {
                UzytkownikId = userId,
                DataUtworzenia = DateTime.UtcNow,
                Pozycje = new List<PozycjaKoszyka>()
            };
            _context.Koszyk.Add(koszyk);
        }

        var istniejaca = koszyk.Pozycje.FirstOrDefault(p => p.TowarId == idTowaru);
        if (istniejaca != null)
        {
            istniejaca.Ilosc += ilosc;
        }
        else
        {
            koszyk.Pozycje.Add(new PozycjaKoszyka
            {
                TowarId = idTowaru,
                Ilosc = ilosc
            });
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
}

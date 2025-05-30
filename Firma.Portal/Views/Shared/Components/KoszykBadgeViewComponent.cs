using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Firma.Portal.ViewComponents;

public class KoszykBadgeViewComponent : ViewComponent
{
    private readonly FirmaContext _context;

    public KoszykBadgeViewComponent(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = "1";

        if (string.IsNullOrEmpty(userId))
        {
            return View(0); // brak zalogowanego użytkownika = 0
        }

        var koszyk = await _context.Koszyk
            .Include(k => k.Pozycje)
            .Where(k => k.UzytkownikId == userId && !k.CzyZamowiony)
            .OrderByDescending(k => k.DataUtworzenia)
            .FirstOrDefaultAsync();

        var count = koszyk?.Pozycje.Sum(p => p.Ilosc) ?? 0;

        return View(count);
    }
}

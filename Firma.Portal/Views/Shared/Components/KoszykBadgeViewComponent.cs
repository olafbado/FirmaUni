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
        // TODO: retrive userId from claims
        string userId = "1";

        int count = await _context.PozycjaKoszyka
            .Where(p => p.Koszyk!.UzytkownikId == userId)
            .SumAsync(p => p.Ilosc);

        return View("Default", count);
    }
}

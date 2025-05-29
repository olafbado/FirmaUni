using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Portal.Controllers;

public class SklepController : Controller
{
    private readonly FirmaContext _context;

    public SklepController(FirmaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string search, int? categoryId, decimal? minPrice, decimal? maxPrice, int page = 1)
    {
        int pageSize = 6;

        var query = _context.Towar.Include(t => t.Rodzaj).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(t => t.Nazwa.Contains(search));

        if (categoryId.HasValue)
            query = query.Where(t => t.IdRodzaju == categoryId.Value);

        if (minPrice.HasValue)
            query = query.Where(t => t.Cena >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(t => t.Cena <= maxPrice.Value);

        var totalItems = await query.CountAsync();
        var produkty = await query
            .OrderBy(t => t.Nazwa)
            // Paginacja, pomiń (page - 1) * pageSize elementów i weź pageSize elementów
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewData["Kategorie"] = _context.Rodzaj.ToList();
        ViewData["TotalPages"] = (int)Math.Ceiling((double)totalItems / pageSize);
        ViewData["CurrentPage"] = page;

        return View(produkty);
    }

}

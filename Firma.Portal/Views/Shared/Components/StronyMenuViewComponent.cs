using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Portal.ViewComponents
{
    public class StronyMenuViewComponent : ViewComponent
    {
        private readonly FirmaContext _context;

        public StronyMenuViewComponent(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var strony = await _context.Strona
                .OrderBy(s => s.Pozycja)
                .Where(s => s.LinkTytul != "start")
                .ToListAsync();

            return View(strony);
        }
    }
}

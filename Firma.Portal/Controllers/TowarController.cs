using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Portal.Controllers
{
    public class TowarController : Controller
    {
        private readonly FirmaContext _context;

        public TowarController(FirmaContext context)

        {
            _context = context;
        }




        public async Task<IActionResult> Details(int id)
        {
            var towar = _context.Towar
                .Include(t => t.Rodzaj)
                .FirstOrDefault(t => t.IdTowaru == id);

            if (towar == null)
                return NotFound();

            var podobne = await _context.Towar
                .Where(t => t.IdRodzaju == towar.IdRodzaju && t.IdTowaru != towar.IdTowaru)
                .Take(3)
                .ToListAsync();

            ViewData["PodobneProdukty"] = podobne;


            return View(towar);
        }
    }
}

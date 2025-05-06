using Firma.Data.Data;
using Firma.Data.Data.Sklep;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers
{
    public class UzytkownikController : Controller
    {
        private readonly FirmaContext _context;

        public UzytkownikController(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Uzytkownik.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var uzytkownik = await _context.Uzytkownik.FirstOrDefaultAsync(u =>
                u.UzytkownikId == id
            );

            if (uzytkownik == null)
                return NotFound();

            return View(uzytkownik);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                _context.Uzytkownik.Add(uzytkownik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(uzytkownik);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var uzytkownik = await _context.Uzytkownik.FindAsync(id);
            if (uzytkownik == null)
                return NotFound();

            return View(uzytkownik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Uzytkownik uzytkownik)
        {
            if (id != uzytkownik.UzytkownikId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(uzytkownik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(uzytkownik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var uzytkownik = await _context.Uzytkownik.FindAsync(id);
            if (uzytkownik != null)
            {
                _context.Uzytkownik.Remove(uzytkownik);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

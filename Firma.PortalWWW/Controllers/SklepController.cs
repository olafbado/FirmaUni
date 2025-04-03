using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Firma.PortalWWW.Controllers
{
    public class SklepController : Controller
    {
        //to jest pole odpowiedzialne za BD
        private readonly FirmaContext _context;
        public SklepController(FirmaContext context)
        {
            _context = context; 
        }
        //to jest funkcja, ktora dostarczy danych widokowi Index
        //bedzie on wyswietlal wszystkie towary rodzaju o danym id (id rodzaju podane jako parametr)
        public async Task<IActionResult> Index(int? id)//id rodzaju towarów, które mają być wyswietlane 
        {
            //pobieramy z bazy danych wszystkie rodzaje towarow 
            //i zapisujemy je do ViewBag, który przeniesie je z Controllera do widoku
            ViewBag.ModelRodzaje = await _context.Rodzaj.ToListAsync();
            //id bedzie wypelniany przy kazdym kliknieciu na rodzaj w sklepie
            //ale przy pierwszym wejsciu do sklpeu bedzie pusty i wtedy decydujemy, ze wyswietlamy towaruy pierwszego rodzaju (docelowo promowane)
            if (id == null)
            {
                id = 1;
            }
            //z bazy danych pobieramy towary o danym idRodzaju (czyli towary danego rodzaju)
            var towaryDanegoRodzaju=await _context.Towar.Where(t=>t.IdRodzaju== id).ToListAsync();
            //towary danego rodzaju przekazujemy do widoku
            return View(towaryDanegoRodzaju);
        }
    }
}

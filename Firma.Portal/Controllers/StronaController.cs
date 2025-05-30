
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firma.Data.Data;
public class StronaController : Controller
{
    private readonly FirmaContext _context;

    public StronaController(FirmaContext context)
    {
        _context = context;
    }

    // Domyślna strona główna
    [HttpGet("/")]
    public async Task<IActionResult> Start()
    {
        var strona = await _context.Strona.FirstOrDefaultAsync(s => s.LinkTytul == "start");
        if (strona == null) return NotFound();
        return View("Pokaz", strona); // Używa tego samego widoku co inne strony
    }

    [HttpGet("/strona/{id}")]
    public async Task<IActionResult> Pokaz(string id)
    {
        var strona = await _context.Strona.FirstOrDefaultAsync(s => s.LinkTytul == id);
        if (strona == null) return NotFound();
        return View(strona);
    }
}
using Firma.Data.Data.CMS;
using Firma.Data.Data.Sklep;
using Microsoft.EntityFrameworkCore;

namespace Firma.Data.Data;

public class FirmaContext : DbContext
{
    public FirmaContext(DbContextOptions<FirmaContext> options)
        : base(options) { }

    public DbSet<Aktualnosc> Aktualnosc { get; set; } = default!;
    public DbSet<Strona> Strona { get; set; } = default!;
    public DbSet<Rodzaj> Rodzaj { get; set; } = default!;
    public DbSet<Towar> Towar { get; set; } = default!;
}

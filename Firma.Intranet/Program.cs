using Firma.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<FirmaContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("FirmaContext") ?? throw new InvalidOperationException("Connection string 'FirmaContext' not found.")));
builder.Services.AddDbContext<FirmaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FirmaContext")));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Firma.Data.Data.FirmaContext>();

    if (!context.Strona.Any())
    {
        context.Strona.AddRange(
            new Firma.Data.Data.CMS.Strona { LinkTytul = "start", Tytul = "Strona główna", Tresc = "Witamy!", Pozycja = 1 },
            new Firma.Data.Data.CMS.Strona { LinkTytul = "onas", Tytul = "O nas", Tresc = "Kilka słów o firmie.", Pozycja = 2 },
            new Firma.Data.Data.CMS.Strona { LinkTytul = "kontakt", Tytul = "Kontakt", Tresc = "Skontaktuj się z nami.", Pozycja = 3 }
        );
    }

    if (!context.Aktualnosc.Any())
    {
        context.Aktualnosc.AddRange(
            new Firma.Data.Data.CMS.Aktualnosc { LinkTytul = "news1", Tytul = "Nowość", Tresc = "Nowy produkt!", Pozycja = 1 },
            new Firma.Data.Data.CMS.Aktualnosc { LinkTytul = "info", Tytul = "Info", Tresc = "Zmiana godzin.", Pozycja = 2 },
            new Firma.Data.Data.CMS.Aktualnosc { LinkTytul = "promo", Tytul = "Promka", Tresc = "Promocja!", Pozycja = 3 }
        );
    }

    if (!context.Rodzaj.Any())
    {
        context.Rodzaj.AddRange(
            new Firma.Data.Data.Sklep.Rodzaj { Nazwa = "Elektronika" },
            new Firma.Data.Data.Sklep.Rodzaj { Nazwa = "Moda" },
            new Firma.Data.Data.Sklep.Rodzaj { Nazwa = "Dom i ogród" }
        );
    }

    if (!context.Towar.Any())
    {
        context.Towar.AddRange(
            new Firma.Data.Data.Sklep.Towar { Nazwa = "Telefon", Cena = 999.99M, Kod = "TEL001", FotoUrl = "telefon.jpg", IdRodzaju = 1 },
            new Firma.Data.Data.Sklep.Towar { Nazwa = "Koszulka", Cena = 49.99M, Kod = "KOSZ001", FotoUrl = "koszulka.jpg", IdRodzaju = 2 },
            new Firma.Data.Data.Sklep.Towar { Nazwa = "Grill", Cena = 299.99M, Kod = "GRILL001", FotoUrl = "grill.jpg", IdRodzaju = 3 }
        );
    }

    context.SaveChanges();
}


app.Run();

using Firma.Data.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region 🔧 KONFIGURACJA USŁUG (Dependency Injection)

// Rejestracja kontekstu bazy danych (PostgreSQL) z pliku appsettings.json
builder.Services.AddDbContext<FirmaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FirmaContext")));

// Dodanie MVC (kontrolery + widoki)
builder.Services.AddControllersWithViews();

#endregion

var app = builder.Build();

#region ⚙️ KONFIGURACJA ŚRODOWISKA

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // obsługa błędów w trybie produkcyjnym
    app.UseHsts(); // wymuszenie HTTPS
}

#endregion

#region 🛠️ MIDDLEWARE (kolejność ma znaczenie!)

// Odpowiednik plugów w Phoenix oraz endpoint.ex i router.ex
app.UseHttpsRedirection();       // przekierowanie na HTTPS
app.UseStaticFiles();            // obsługa wwwroot/
app.UseRouting();                // routing MVC
app.UseAuthorization();          // autoryzacja (opcjonalna, na przyszłość)

#endregion

#region 📍 ROUTING GŁÓWNY (domyślny)

// odpowiednik `scope "/users", AppWeb do` w Phoenix
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


#endregion

#region 🌱 SEEDOWANIE DANYCH STARTOWYCH

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FirmaContext>();

    // Strony CMS
    if (!context.Strona.Any())
    {
        context.Strona.AddRange(
            new Firma.Data.Data.CMS.Strona { LinkTytul = "start", Tytul = "Strona główna", Tresc = "Witamy!", Pozycja = 1 },
            new Firma.Data.Data.CMS.Strona { LinkTytul = "onas", Tytul = "O nas", Tresc = "Kilka słów o firmie.", Pozycja = 2 },
            new Firma.Data.Data.CMS.Strona { LinkTytul = "kontakt", Tytul = "Kontakt", Tresc = "Skontaktuj się z nami.", Pozycja = 3 }
        );
    }

    // Aktualności
    if (!context.Aktualnosc.Any())
    {
        context.Aktualnosc.AddRange(
            new Firma.Data.Data.CMS.Aktualnosc { LinkTytul = "news1", Tytul = "Nowość", Tresc = "Nowy produkt!", Pozycja = 1 },
            new Firma.Data.Data.CMS.Aktualnosc { LinkTytul = "info", Tytul = "Info", Tresc = "Zmiana godzin.", Pozycja = 2 },
            new Firma.Data.Data.CMS.Aktualnosc { LinkTytul = "promo", Tytul = "Promka", Tresc = "Promocja!", Pozycja = 3 }
        );
    }

    // Rodzaje produktów
    if (!context.Rodzaj.Any())
    {
        context.Rodzaj.AddRange(
            new Firma.Data.Data.Sklep.Rodzaj { Nazwa = "Elektronika" },
            new Firma.Data.Data.Sklep.Rodzaj { Nazwa = "Moda" },
            new Firma.Data.Data.Sklep.Rodzaj { Nazwa = "Dom i ogród" }
        );
    }

    // Towary
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

#endregion

app.Run(); // 🔚 Start aplikacji

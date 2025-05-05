using Firma.Data;
using Firma.Data.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region 🔧 KONFIGURACJA USŁUG (Dependency Injection)

// Rejestracja kontekstu bazy danych (PostgreSQL) z pliku appsettings.json
builder.Services.AddDbContext<FirmaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FirmaContext"))
);

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
app.UseHttpsRedirection(); // przekierowanie na HTTPS
app.UseStaticFiles(); // obsługa wwwroot/
app.UseRouting(); // routing MVC
app.UseAuthorization(); // autoryzacja (opcjonalna, na przyszłość)
#endregion

#region 📍 ROUTING GŁÓWNY (domyślny)

// odpowiednik `scope "/users", AppWeb do` w Phoenix
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

#endregion

#region 🌱 SEEDOWANIE DANYCH STARTOWYCH

using (var scope = app.Services.CreateScope())
{
    SeedData.Initialize(scope.ServiceProvider);
}
#endregion

app.Run(); // 🔚 Start aplikacji

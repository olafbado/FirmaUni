using Firma.Data.Data; // dostęp do DbContext z Firma.Data
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Rejestracja DbContext z PostgreSQL
builder.Services.AddDbContext<FirmaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FirmaContext")));

// Rejestracja MVC (kontrolery + widoki)
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();


// Obsługa błędów w środowisku produkcyjnym
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Obsługa HTTPS i plików statycznych
app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing i autoryzacja
app.UseRouting();
app.UseAuthorization();

// Domyślny routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSession();

app.Run();

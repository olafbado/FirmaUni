# Run app
cd Firma.Intranet
dotnet clean && dotnet build && dotnet watch run
cd ../Firma.Portal
dotnet clean && dotnet build && dotnet watch run


# Asynchroniczność i wątki w ASP.NET Core (async/await)

## 🔍 Czym jest `async/await` i jak działa w ASP.NET Core?

Asynchroniczność pozwala na bardziej efektywne wykorzystanie zasobów serwera, w szczególności **wątków**.

## 🧵 Co to jest wątek?

- **Wątek** to najmniejsza jednostka wykonywania w systemie operacyjnym.
- **Proces** to uruchomiona aplikacja (np. serwer, edytor tekstu, przeglądarka).
- Jeden **proces** może mieć **wiele wątków**, które działają niezależnie i równolegle.
- **Wątki wykonują konkretne zadania** w obrębie danego procesu.
- Dzięki wielu wątkom aplikacja może robić wiele rzeczy jednocześnie – np. obsługiwać wielu użytkowników na raz.


## ✅ Kluczowe założenia

- `async/await` pozwala wykonać operację (np. zapytanie do bazy danych) **bez blokowania** aktualnego wątku.
- Wątek, który rozpoczął operację, **wraca do puli** dostępnych wątków i może zająć się innym zadaniem.
- Gdy operacja async (np. `await _context.Towar.ToListAsync()`) się zakończy, jej kontynuacja zostanie **przejęta przez inny wolny wątek**.
- Dzięki temu **żaden wątek nie stoi bezczynnie** czekając na zakończenie I/O lub operacji zewnętrznych.
- To rozwiązanie **zwiększa skalowalność** i **wydajność** aplikacji – serwer może obsłużyć więcej użytkowników przy mniejszym zużyciu zasobów.

## 💡 Przykład praktyczny

```csharp
public async Task<IActionResult> Index()
{
    var towarList = await _context.Towar.Include(t => t.Rodzaj).ToListAsync();
    return View(towarList);
} 


## 🔄 Porównanie przepływu requesta: Phoenix (Elixir) vs ASP.NET Core MVC

Jeśli znasz Phoenix, to oto jak możesz zrozumieć ASP.NET Core MVC przez analogię:

| Phoenix                     | ASP.NET Core MVC                               | Opis                                              |
| --------------------------- | ---------------------------------------------- | ------------------------------------------------- |
| `endpoint.ex`               | `Program.cs` (`app.UseX`)                      | Konfiguracja middleware'ów (pipeline requestów)   |
| `router.ex`                 | `app.MapControllerRoute(...)`                  | Mapowanie ścieżek URL na kontrolery i akcje       |
| `Controller` (moduł)        | `Controller` (klasa)                           | Kontroler zawierający logikę dla widoków          |
| `def index(conn, _params)`  | `public IActionResult Index()`                 | Akcja obsługująca konkretny endpoint              |
| `render(conn, "view.html")` | `return View(model)`                           | Renderowanie widoku z danymi                      |
| `.heex` / `.eex`            | `.cshtml` (Razor)                              | Szablony HTML z możliwością osadzania kodu        |
| `app.html.heex`             | `_Layout.cshtml`                               | Główny layout aplikacji z `@RenderBody()`         |
| `Plug`                      | Middleware (`UseX()`)                          | Warstwa przetwarzania requestów przed kontrolerem |
| `conn` + `assigns`          | `HttpContext` + `ViewData`, `ViewBag`, `Model` | Dane przekazywane do widoku                       |

### 🔁 Przykład przepływu w ASP.NET

1. Klient wysyła request do `/Towar/Index`
2. `MapControllerRoute` dopasowuje `{controller=Towar}/{action=Index}`
3. `TowarController.Index()` zostaje wywołany
4. Pobierane są dane z bazy (`DbContext`)
5. Dane są przekazywane do widoku `Views/Towar/Index.cshtml`
6. Widok jest renderowany z layoutem `_Layout.cshtml`
7. Odpowiedź HTML wraca do przeglądarki


# MIGRAIONS
Add migraion: > dotnet ef migrations add UpdateZamowinie --project Firma.Data --startup-project Firma.Intranet
Run migration: > dotnet ef database update --project Firma.Data --startup-project Firma.Intranet


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

````csharp
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
Drop database:
> dotnet ef database drop --project Firma.Data --startup-project Firma.Intranet
# Create database

```bash
dotnet ef database update --project Firma.Data --startup-project Firma.Intranet
````

This command applies all pending migrations and creates the database if it doesn't exist.

Add migraion: > dotnet ef migrations add UpdateZamowinie --project Firma.Data --startup-project Firma.Intranet
Run migration: > dotnet ef database update --project Firma.Data --startup-project Firma.Intranet

✅ Zadanie 1 – Obsługa minimum 6 klas w projekcie Portal WWW

1. Towar – model produktu wyświetlany na liście i szczegółach
2. Rodzaj – kategorie produktów, wykorzystywane w filtrach
3. Koszyk – reprezentacja koszyka użytkownika
4. PozycjaKoszyka – relacja produkt–ilość w koszyku
5. Zamowienie – dane zamówienia składane przez użytkownika
6. PozycjaZamowienia – szczegóły produktów przypisanych do zamówienia
7. Uzytkownik – podstawowy model użytkownika

✅ Zadanie 2 – Profesjonalny i dopracowany wygląd widoków

1. Strona główna sklepu (/Sklep) - Filtrowanie, paginacja, responsywne karty, estetyczne zdjęcia
2. Szczegóły produktu (/Towar/Details/{id}) - Przejrzysty layout, wyraźne CTA, podobne produkty
3. Koszyk (/Koszyk/Index) - Czytelna tabela, suma, przycisk do zamówienia
4. Zamówienie (/Zamowienie/Nowe) - Formularz zamówienia z walidacją i designem
5. Nawigacja - Komponent z ikoną koszyka + badge + live update

✅ Zadanie 3 – Sterowanie tekstami

W Portal WWW skupilem sie na kompletnym procesie uzytkownika, od wyswietlenia produktow, przez dodanie ich do koszyka po zlozenie zamowienia.

## 🛍️ Funkcjonalności strony /sklep

Strona `/sklep` pełni rolę głównego katalogu produktów i umożliwia użytkownikom przeglądanie oraz filtrowanie oferty. Poniżej lista planowanych funkcji:

### ✅ Kluczowe funkcjonalności:

| Funkcja                      | Opis                                                                               |
| ---------------------------- | ---------------------------------------------------------------------------------- |
| **Wyświetlanie produktów**   | Siatka kart produktowych z nazwą, zdjęciem, ceną i przyciskiem „Dodaj do koszyka”. |
| **Filtrowanie po kategorii** | Lista kategorii (`Rodzaj`) w bocznym panelu – umożliwia zawężenie wyników.         |
| **Filtrowanie po cenie**     | Zakres cenowy – np. suwaki lub pola liczbowego `Od` / `Do`.                        |
| **Wyszukiwanie**             | Pole wyszukiwania z dynamicznym filtrowaniem po nazwie lub opisie produktu.        |
| **Paginacja**                | Stronicowanie wyników – np. po 9/12 produktów na stronę.                           |
| **Dodawanie do koszyka**     | Każda karta produktu zawiera przycisk umożliwiający dodanie do koszyka.            |

### 💡 Informacje techniczne:
- Filtrowanie i paginacja realizowane są poprzez `GET` i `query string` (np. `?kategoria=1&minCena=10&maxCena=100&page=2`).
- Koszyk użytkownika może być przechowywany w sesji (`HttpContext.Session`) lub jako ciasteczko.
- Kategorie (`Rodzaj`) i produkty (`Towar`) pobierane z bazy danych (Entity Framework Core).
- Styl i układ zgodny z resztą Portalu – spójna paleta kolorów i komponenty Bootstrap.


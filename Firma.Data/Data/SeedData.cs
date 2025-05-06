using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Firma.Data.Data.Sklep;
using Microsoft.Extensions.DependencyInjection;

namespace Firma.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<FirmaContext>();

            // Usuwanie istniejących danych (opcjonalne — przydatne do testów)
            context.Uzytkownik.RemoveRange(context.Uzytkownik);
            context.Strona.RemoveRange(context.Strona);
            context.Aktualnosc.RemoveRange(context.Aktualnosc);
            context.Rodzaj.RemoveRange(context.Rodzaj);
            context.Towar.RemoveRange(context.Towar);
            context.Koszyk.RemoveRange(context.Koszyk);
            context.Zamowienie.RemoveRange(context.Zamowienie);
            context.SaveChanges();

            // Dodaj użytkownika testowego
            context.Uzytkownik.Add(
                new Uzytkownik
                {
                    UzytkownikId = "1",
                    Imie = "UserTest",
                    Nazwisko = "UserTest",
                    Email = "test@test.com",
                }
            );
            context.SaveChanges();

            // CMS - Strony
            context.Strona.AddRange(
                new Strona
                {
                    LinkTytul = "start",
                    Tytul = "Strona główna",
                    Tresc = "Witamy!",
                    Pozycja = 1,
                },
                new Strona
                {
                    LinkTytul = "onas",
                    Tytul = "O nas",
                    Tresc = "Kilka słów o firmie.",
                    Pozycja = 2,
                },
                new Strona
                {
                    LinkTytul = "kontakt",
                    Tytul = "Kontakt",
                    Tresc = "Skontaktuj się z nami.",
                    Pozycja = 3,
                }
            );

            // CMS - Aktualności
            context.Aktualnosc.AddRange(
                new Aktualnosc
                {
                    LinkTytul = "news1",
                    Tytul = "Nowość",
                    Tresc = "Nowy produkt!",
                    Pozycja = 1,
                },
                new Aktualnosc
                {
                    LinkTytul = "info",
                    Tytul = "Info",
                    Tresc = "Zmiana godzin.",
                    Pozycja = 2,
                },
                new Aktualnosc
                {
                    LinkTytul = "promo",
                    Tytul = "Promka",
                    Tresc = "Promocja!",
                    Pozycja = 3,
                }
            );

            // Sklep - Kategorie
            context.Rodzaj.AddRange(
                new Rodzaj { IdRodzaju = 1, Nazwa = "Elektronika" },
                new Rodzaj { IdRodzaju = 2, Nazwa = "Moda" },
                new Rodzaj { IdRodzaju = 3, Nazwa = "Dom i ogród" }
            );

            // Sklep - Towary
            context.Towar.AddRange(
                new Towar
                {
                    IdTowaru = 1,
                    Nazwa = "Telefon",
                    Cena = 999.99M,
                    Kod = "TEL001",
                    FotoUrl = "telefon.jpg",
                    IdRodzaju = 1,
                    Ilosc = 10,
                },
                new Towar
                {
                    IdTowaru = 2,
                    Nazwa = "Koszulka",
                    Cena = 49.99M,
                    Kod = "KOSZ001",
                    FotoUrl = "koszulka.jpg",
                    IdRodzaju = 2,
                    Ilosc = 3,
                },
                new Towar
                {
                    IdTowaru = 3,
                    Nazwa = "Grill",
                    Cena = 299.99M,
                    Kod = "GRILL001",
                    FotoUrl = "grill.jpg",
                    IdRodzaju = 3,
                    Ilosc = 2,
                }
            );

            // Koszyk testowy
            context.Koszyk.Add(
                new Koszyk
                {
                    UzytkownikId = "1",
                    Pozycje = new List<PozycjaKoszyka>
                    {
                        new PozycjaKoszyka { TowarId = 1, Ilosc = 2 },
                        new PozycjaKoszyka { TowarId = 2, Ilosc = 1 },
                    },
                }
            );

            // Zamówienie testowe
            context.Zamowienie.Add(
                new Zamowienie
                {
                    Suma = 1349.97M,
                    UzytkownikId = "1",
                    Pozycje = new List<PozycjaZamowienia>
                    {
                        new PozycjaZamowienia
                        {
                            TowarId = 1,
                            Ilosc = 1,
                            CenaJednostkowa = 999.99M,
                        },
                        new PozycjaZamowienia
                        {
                            TowarId = 2,
                            Ilosc = 2,
                            CenaJednostkowa = 49.99M,
                        },
                        new PozycjaZamowienia
                        {
                            TowarId = 3,
                            Ilosc = 1,
                            CenaJednostkowa = 299.99M,
                        },
                    },
                }
            );

            context.SaveChanges();
        }
    }
}

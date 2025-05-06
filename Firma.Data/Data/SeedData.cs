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

            // Czyścimy dane
            context.Uzytkownik.RemoveRange(context.Uzytkownik);
            context.Strona.RemoveRange(context.Strona);
            context.Aktualnosc.RemoveRange(context.Aktualnosc);
            context.Rodzaj.RemoveRange(context.Rodzaj);
            context.Towar.RemoveRange(context.Towar);
            context.Koszyk.RemoveRange(context.Koszyk);
            context.Zamowienie.RemoveRange(context.Zamowienie);
            context.SaveChanges();

            // Użytkownik
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

            // Strony CMS
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

            // Aktualności
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

            // Kategorie
            var rodzaj1 = new Rodzaj { Nazwa = "Elektronika" };
            var rodzaj2 = new Rodzaj { Nazwa = "Moda" };
            var rodzaj3 = new Rodzaj { Nazwa = "Dom i ogród" };

            context.Rodzaj.AddRange(rodzaj1, rodzaj2, rodzaj3);
            context.SaveChanges();

            // Towary
            var towar1 = new Towar
            {
                Nazwa = "Telefon",
                Cena = 999.99M,
                Kod = "TEL001",
                FotoUrl = "telefon.jpg",
                IdRodzaju = rodzaj1.IdRodzaju,
                Ilosc = 10,
            };
            var towar2 = new Towar
            {
                Nazwa = "Koszulka",
                Cena = 49.99M,
                Kod = "KOSZ001",
                FotoUrl = "koszulka.jpg",
                IdRodzaju = rodzaj2.IdRodzaju,
                Ilosc = 3,
            };
            var towar3 = new Towar
            {
                Nazwa = "Grill",
                Cena = 299.99M,
                Kod = "GRILL001",
                FotoUrl = "grill.jpg",
                IdRodzaju = rodzaj3.IdRodzaju,
                Ilosc = 2,
            };

            context.Towar.AddRange(towar1, towar2, towar3);
            context.SaveChanges();

            // Koszyk
            context.Koszyk.Add(
                new Koszyk
                {
                    UzytkownikId = "1",
                    Pozycje = new List<PozycjaKoszyka>
                    {
                        new PozycjaKoszyka { TowarId = towar1.IdTowaru, Ilosc = 2 },
                        new PozycjaKoszyka { TowarId = towar2.IdTowaru, Ilosc = 1 },
                    },
                }
            );

            // Zamówienie
            context.Zamowienie.Add(
                new Zamowienie
                {
                    UzytkownikId = "1",
                    Suma = 1349.97M,
                    Pozycje = new List<PozycjaZamowienia>
                    {
                        new PozycjaZamowienia
                        {
                            TowarId = towar1.IdTowaru,
                            Ilosc = 1,
                            CenaJednostkowa = 999.99M,
                        },
                        new PozycjaZamowienia
                        {
                            TowarId = towar2.IdTowaru,
                            Ilosc = 2,
                            CenaJednostkowa = 49.99M,
                        },
                        new PozycjaZamowienia
                        {
                            TowarId = towar3.IdTowaru,
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

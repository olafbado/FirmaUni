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

            // Dodaj użytkownika testowego
            if (!context.Uzytkownik.Any())
            {
                context.Uzytkownik.Add(
                    new Uzytkownik
                    {
                        Id = "1",
                        Imie = "UserTest",
                        Nazwisko = "UserTest",
                        Email = "test@test.com",
                    }
                );
                context.SaveChanges();
            }

            // CMS
            if (!context.Strona.Any())
            {
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
            }

            if (!context.Aktualnosc.Any())
            {
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
            }

            // Sklep
            if (!context.Rodzaj.Any())
            {
                context.Rodzaj.AddRange(
                    new Rodzaj { Nazwa = "Elektronika" },
                    new Rodzaj { Nazwa = "Moda" },
                    new Rodzaj { Nazwa = "Dom i ogród" }
                );
            }

            if (!context.Towar.Any())
            {
                context.Towar.AddRange(
                    new Towar
                    {
                        Nazwa = "Telefon",
                        Cena = 999.99M,
                        Kod = "TEL001",
                        FotoUrl = "telefon.jpg",
                        IdRodzaju = 1,
                    },
                    new Towar
                    {
                        Nazwa = "Koszulka",
                        Cena = 49.99M,
                        Kod = "KOSZ001",
                        FotoUrl = "koszulka.jpg",
                        IdRodzaju = 2,
                    },
                    new Towar
                    {
                        Nazwa = "Grill",
                        Cena = 299.99M,
                        Kod = "GRILL001",
                        FotoUrl = "grill.jpg",
                        IdRodzaju = 3,
                    }
                );
            }

            if (!context.Koszyk.Any())
            {
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
            }

            if (!context.Zamowienie.Any())
            {
                context.Zamowienie.Add(
                    new Zamowienie
                    {
                        ImieNazwisko = "Jan Kowalski",
                        Email = "jan.kowalski@example.com",
                        Adres = "ul. Przykładowa 123, Warszawa",
                        Suma = 1349.97M,
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
                                Ilosc = 1,
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
            }

            context.SaveChanges();
        }
    }
}

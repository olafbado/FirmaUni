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
    Opis = "Nowoczesny smartfon z dużym ekranem i szybkim procesorem. Idealny do pracy i rozrywki. Posiada aparat o wysokiej rozdzielczości."
};

var towar1b = new Towar
{
    Nazwa = "Laptop",
    Cena = 2999.99M,
    Kod = "LAP001",
    FotoUrl = "laptop.jpg",
    IdRodzaju = rodzaj1.IdRodzaju,
    Ilosc = 5,
    Opis = "Wydajny laptop z procesorem Intel i7 i dyskiem SSD. Doskonały do codziennej pracy i grania. Posiada ekran Full HD i solidną obudowę."
};

var towar1c = new Towar
{
    Nazwa = "Słuchawki",
    Cena = 199.99M,
    Kod = "SLUCH001",
    FotoUrl = "sluchawki.jpg",
    IdRodzaju = rodzaj1.IdRodzaju,
    Ilosc = 15,
    Opis = "Bezprzewodowe słuchawki z funkcją redukcji szumów. Zapewniają czysty dźwięk i długi czas pracy na baterii. Wygodne na dłuższe sesje słuchania."
};

var towar2 = new Towar
{
    Nazwa = "Koszulka",
    Cena = 49.99M,
    Kod = "KOSZ001",
    FotoUrl = "koszulka.jpg",
    IdRodzaju = rodzaj2.IdRodzaju,
    Ilosc = 3,
    Opis = "Bawełniana koszulka dostępna w różnych kolorach. Miękka i przyjemna w noszeniu. Idealna na każdą porę roku."
};

var towar2b = new Towar
{
    Nazwa = "Spodnie",
    Cena = 99.99M,
    Kod = "SPOD001",
    FotoUrl = "spodnie.jpg",
    IdRodzaju = rodzaj2.IdRodzaju,
    Ilosc = 7,
    Opis = "Wygodne spodnie jeansowe o klasycznym kroju. Świetnie pasują do codziennych stylizacji. Trwały materiał i nowoczesny design."
};

var towar2c = new Towar
{
    Nazwa = "Buty",
    Cena = 199.99M,
    Kod = "BUTY001",
    FotoUrl = "buty.jpg",
    IdRodzaju = rodzaj2.IdRodzaju,
    Ilosc = 4,
    Opis = "Sportowe buty z oddychającego materiału. Oferują wysoki komfort użytkowania. Idealne na spacery i treningi."
};

var towar3 = new Towar
{
    Nazwa = "Grill",
    Cena = 299.99M,
    Kod = "GRILL001",
    FotoUrl = "grill.jpg",
    IdRodzaju = rodzaj3.IdRodzaju,
    Ilosc = 2,
    Opis = "Solidny grill ogrodowy z rusztem ze stali nierdzewnej. Umożliwia szybkie i wygodne przygotowanie potraw. Łatwy w czyszczeniu i montażu."
};

var towar3b = new Towar
{
    Nazwa = "Leżak",
    Cena = 149.99M,
    Kod = "LEZAK001",
    FotoUrl = "lezak.jpg",
    IdRodzaju = rodzaj3.IdRodzaju,
    Ilosc = 6,
    Opis = "Składany leżak ogrodowy z regulowanym oparciem. Idealny do relaksu na świeżym powietrzu. Lekki i łatwy do przechowywania."
};

var towar3c = new Towar
{
    Nazwa = "Doniczka",
    Cena = 29.99M,
    Kod = "DONICZKA001",
    FotoUrl = "doniczka.jpg",
    IdRodzaju = rodzaj3.IdRodzaju,
    Ilosc = 12,
    Opis = "Elegancka doniczka ceramiczna w nowoczesnym stylu. Pasuje do każdego wnętrza i ogrodu. Odporna na warunki atmosferyczne."
};

var towar4c = new Towar
{
    Nazwa = "Kosiarka",
    Cena = 123,
    Kod = "KOSIARKA",
    FotoUrl = "kosiarka.jpg",
    IdRodzaju = rodzaj3.IdRodzaju,
    Ilosc = 2,
    Opis = "Praktyczna kosiarka elektryczna do małych i średnich ogrodów. Wyposażona w regulację wysokości koszenia. Cicha i łatwa w obsłudze."
};

            

            context.Towar.AddRange(towar1, towar2, towar3, 
                towar1b, towar1c, towar2b, towar2c, towar3b, towar3c,towar4c);
            context.SaveChanges();

            // Koszyk
            var koszyk = new Koszyk
            {
                UzytkownikId = "1",
                Pozycje = new List<PozycjaKoszyka>
                {
                    new PozycjaKoszyka { TowarId = towar1.IdTowaru, Ilosc = 2 },
                    new PozycjaKoszyka { TowarId = towar2.IdTowaru, Ilosc = 1 },
                },
            };
            context.Koszyk.Add(koszyk);
            context.SaveChanges();

            context.Zamowienie.Add(
                new Zamowienie
                {
                    UzytkownikId = "1",
                    Suma = 1349.97M,
                    Adres = "ul. Testowa 1, 00-001 Warszawa",
                    MetodaPlatnosci = "Karta kredytowa",
                    KoszykId = koszyk.IdKoszyka,
                }
            );

            context.SaveChanges();
        }
    }
}

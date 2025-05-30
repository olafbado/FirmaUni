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
                    Tresc = """
                        <section class="hero mb-5">
                        <div class="container text-center">
                            <h1 class="display-4">Witamy w naszej firmie</h1>
                            <p class="lead">Twój zaufany partner w branży.</p>
                            <a href="/sklep" class="btn btn-primary btn-lg mt-3">Przejdź do sklepu</a>
                        </div>
                    </section>

                    <div class="container">
                        <div class="row mb-5 align-items-center">
                            <div class="col-md-6 mb-4 mb-md-0">
                                <div class="placeholder-box"></div>
                            </div>
                            <div class="col-md-6">
                                <h2>Nasza misja</h2>
                                <p>Zapewniamy najwyższy poziom usług, zorientowany na potrzeby klienta i zrównoważony rozwój.</p>
                            </div>
                        </div>

                        <div class="row mb-5 align-items-center flex-md-row-reverse">
                            <div class="col-md-6 mb-4 mb-md-0">
                                <div class="placeholder-box"></div>
                            </div>
                            <div class="col-md-6">
                                <h2>Dlaczego my?</h2>
                                <p>Wieloletnie doświadczenie i pasja sprawiają, że nasi klienci czują się u nas jak w domu.</p>
                            </div>
                        </div>

                        <div class="row text-center my-5">
                            <div class="col-md-4">
                                <i class="bi bi-award display-5 text-success"></i>
                                <h5 class="mt-3">Jakość</h5>
                                <p>Produkty najwyższej klasy.</p>
                            </div>
                            <div class="col-md-4">
                                <i class="bi bi-globe2 display-5 text-success"></i>
                                <h5 class="mt-3">Zasięg</h5>
                                <p>Działamy lokalnie i globalnie.</p>
                            </div>
                            <div class="col-md-4">
                                <i class="bi bi-heart display-5 text-success"></i>
                                <h5 class="mt-3">Zaufanie</h5>
                                <p>Setki zadowolonych klientów.</p>
                            </div>
                        </div>

                        <section class="container my-5 py-4 bg-white rounded shadow">
                            <div class="row align-items-center">
                                <div class="col-md-6 mb-4 mb-md-0">
                                    <div class="placeholder-box"></div>
                                </div>
                                <div class="col-md-6">
                                    <h3 class="mb-4">Dlaczego warto nam zaufać?</h3>
                                    <ul class="list-unstyled">
                                        <li class="mb-3 d-flex">
                                            <i class="bi bi-check-circle-fill text-success me-2 fs-5"></i>
                                            <span>Wieloletnie doświadczenie i eksperci w branży</span>
                                        </li>
                                        <li class="mb-3 d-flex">
                                            <i class="bi bi-check-circle-fill text-success me-2 fs-5"></i>
                                            <span>Indywidualne podejście do każdego klienta</span>
                                        </li>
                                        <li class="mb-3 d-flex">
                                            <i class="bi bi-check-circle-fill text-success me-2 fs-5"></i>
                                            <span>Gwarancja jakości i bezpieczeństwa</span>
                                        </li>
                                        <li class="mb-3 d-flex">
                                            <i class="bi bi-check-circle-fill text-success me-2 fs-5"></i>
                                            <span>Nowoczesne technologie i ciągły rozwój</span>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </section>

                        <section class="container my-5 py-4">
                            <h3 class="text-center mb-4">Opinie naszych klientów</h3>
                            <div class="row g-4">
                                <div class="col-md-4">
                                    <div class="bg-white p-4 shadow-sm rounded h-100">
                                        <p class="fst-italic">"Profesjonalna obsługa i szybki kontakt. Polecam każdemu!"</p>
                                        <div class="mt-3 d-flex align-items-center">
                                            <div class="rounded-circle placeholder-box me-3" style="width: 50px; height: 50px;"></div>
                                            <div>
                                                <strong>Anna Kowalska</strong><br />
                                                <small>Warszawa</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="bg-white p-4 shadow-sm rounded h-100">
                                        <p class="fst-italic">"Świetna jakość usług i indywidualne podejście. Na pewno wrócę!"</p>
                                        <div class="mt-3 d-flex align-items-center">
                                            <div class="rounded-circle placeholder-box me-3" style="width: 50px; height: 50px;"></div>
                                            <div>
                                                <strong>Marek Nowak</strong><br />
                                                <small>Kraków</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="bg-white p-4 shadow-sm rounded h-100">
                                        <p class="fst-italic">"Pełne zaufanie i świetna komunikacja na każdym etapie współpracy."</p>
                                        <div class="mt-3 d-flex align-items-center">
                                            <div class="rounded-circle placeholder-box me-3" style="width: 50px; height: 50px;"></div>
                                            <div>
                                                <strong>Julia Zielińska</strong><br />
                                                <small>Gdańsk</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                    """,
                    Pozycja = 1,
                },
                new Strona
                {
                    LinkTytul = "onas",
                    Tytul = "O nas",
                    Tresc = """
                        <section class="container">
                        <h1 class="display-4 mb-4 text-center">Kim jesteśmy?</h1>

                        <div class="row align-items-center mb-5">
                            <div class="col-md-6 mb-4 mb-md-0">
                                <div class="bg-secondary rounded w-100" style="height: 250px;"></div>
                            </div>
                            <div class="col-md-6">
                                <p>
                                    Jesteśmy zespołem pasjonatów, którzy od ponad dekady tworzą rozwiązania dopasowane do potrzeb klientów z
                                    różnych branż.
                                    Nasza firma opiera się na wartościach: **jakość, zaufanie, rozwój**.
                                </p>
                                <p>
                                    Łączymy doświadczenie z nowoczesnym podejściem i technologiami. Dzięki temu nasi klienci mogą liczyć nie
                                    tylko na sprawdzoną ofertę, ale także partnerską współpracę.
                                </p>
                            </div>
                        </div>

                        <div class="bg-white p-4 mb-5 shadow-sm rounded">
                            <blockquote class="blockquote mb-0 text-center">
                                <p class="fst-italic">"Najlepszą inwestycją są relacje z ludźmi. Reszta przychodzi sama."</p>
                                <footer class="blockquote-footer mt-2">Założyciel firmy</footer>
                            </blockquote>
                        </div>

                        <h2 class="text-center mb-4">Nasza firma w liczbach</h2>
                        <div class="row text-center g-4 mb-5">
                            <div class="col-md-3">
                                <div class="bg-success text-white p-4 rounded shadow-sm">
                                    <h3 class="mb-0">12+</h3>
                                    <small>Lat doświadczenia</small>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="bg-success text-white p-4 rounded shadow-sm">
                                    <h3 class="mb-0">500+</h3>
                                    <small>Zrealizowanych projektów</small>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="bg-success text-white p-4 rounded shadow-sm">
                                    <h3 class="mb-0">30+</h3>
                                    <small>Ekspertów w zespole</small>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="bg-success text-white p-4 rounded shadow-sm">
                                    <h3 class="mb-0">98%</h3>
                                    <small>Pozytywnych opinii</small>
                                </div>
                            </div>
                        </div>

                        <div class="row align-items-center mb-5">
                            <div class="col-md-6">
                                <h3>Nasze podejście</h3>
                                <p>
                                    Pracujemy blisko z klientem. Każdy projekt zaczynamy od rozmowy i analizy. Dopiero potem tworzymy
                                    strategię, która pasuje do realnych potrzeb.
                                </p>
                                <ul>
                                    <li>Indywidualne podejście</li>
                                    <li>Transparentność i zaufanie</li>
                                    <li>Elastyczność w działaniu</li>
                                </ul>
                            </div>
                            <div class="col-md-6">
                                <div class="bg-secondary rounded w-100" style="height: 250px;"></div>
                            </div>
                        </div>
                    </section>
                    """,
                    Pozycja = 2,
                },
                new Strona
                {
                    LinkTytul = "uslugi",
                    Tytul = "Nasze uslugi",
                    Tresc = """
                            <section class="container">
                            <h1 class="display-4 text-center mb-4">Nasze usługi</h1>
                            <p class="lead text-center mb-5">
                                Oferujemy kompleksowe rozwiązania dopasowane do Twoich potrzeb.
                                Sprawdź, co możemy dla Ciebie zrobić!
                            </p>

                            <div class="row g-4 mb-5">
                                <div class="col-md-4">
                                    <div class="bg-white rounded shadow-sm p-4 h-100 text-center">
                                        <i class="bi bi-gear-fill text-success fs-1 mb-3"></i>
                                        <h5 class="mb-3">Doradztwo techniczne</h5>
                                        <p>Pomagamy w wyborze najlepszych rozwiązań technologicznych. Skonsultuj się z naszym ekspertem!</p>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="bg-white rounded shadow-sm p-4 h-100 text-center">
                                        <i class="bi bi-laptop-fill text-success fs-1 mb-3"></i>
                                        <h5 class="mb-3">Projektowanie i wdrożenie</h5>
                                        <p>Od koncepcji po realizację – tworzymy rozwiązania od A do Z, które działają.</p>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="bg-white rounded shadow-sm p-4 h-100 text-center">
                                        <i class="bi bi-shield-lock-fill text-success fs-1 mb-3"></i>
                                        <h5 class="mb-3">Wsparcie i bezpieczeństwo</h5>
                                        <p>Dbamy o Twoje dane i ciągłość działania systemów. Możesz spać spokojnie.</p>
                                    </div>
                                </div>
                            </div>

                            <div class="row align-items-center mb-5">
                                <div class="col-md-6 mb-4 mb-md-0">
                                    <div class="bg-secondary rounded w-100" style="height: 250px;"></div>
                                </div>
                                <div class="col-md-6">
                                    <h3>Dlaczego warto z nami współpracować?</h3>
                                    <ul class="list-unstyled mt-3">
                                        <li class="mb-2 d-flex align-items-start">
                                            <i class="bi bi-check-circle-fill text-success me-2 mt-1"></i>
                                            <span>Elastyczne podejście – dopasowujemy się do Ciebie.</span>
                                        </li>
                                        <li class="mb-2 d-flex align-items-start">
                                            <i class="bi bi-check-circle-fill text-success me-2 mt-1"></i>
                                            <span>Wieloletnie doświadczenie w branży.</span>
                                        </li>
                                        <li class="mb-2 d-flex align-items-start">
                                            <i class="bi bi-check-circle-fill text-success me-2 mt-1"></i>
                                            <span>Transparentność i uczciwe warunki współpracy.</span>
                                        </li>
                                        <li class="mb-2 d-flex align-items-start">
                                            <i class="bi bi-check-circle-fill text-success me-2 mt-1"></i>
                                            <span>Szybki czas reakcji i sprawna komunikacja.</span>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="text-center mt-5">
                                <a href="/kontakt" class="btn btn-primary btn-lg">
                                    Skontaktuj się z nami <i class="bi bi-arrow-right ms-2"></i>
                                </a>
                            </div>
                    </section>

                    """,
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
                towar1b, towar1c, towar2b, towar2c, towar3b, towar3c, towar4c);
            context.SaveChanges();

            // Koszyk
            var koszyk = new Koszyk
            {
                UzytkownikId = "1",
                CzyZamowiony = true,
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

using Microsoft.AspNetCore.Mvc;
using Firma.Data.Data;
using Firma.Portal.ViewModel;
using Firma.Portal.Helpers;


public class KoszykController : Controller
{
    private readonly FirmaContext _context;
    private const string SessionKey = "Koszyk";

    public KoszykController(FirmaContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var koszyk = HttpContext.Session.GetObject<List<PozycjaKoszykaVM>>(SessionKey) ?? new();
        return View(koszyk);
    }

    [HttpPost]
    public IActionResult Dodaj(int idTowaru, int ilosc)
    {
        var towar = _context.Towar.FirstOrDefault(t => t.IdTowaru == idTowaru);
        if (towar == null || ilosc < 1)
        {
            return RedirectToAction("Index", "Sklep");
        }

        // Pobierz obecny koszyk z sesji
        var koszyk = HttpContext.Session.GetObject<List<PozycjaKoszykaVM>>("Koszyk") ?? new();

        // Szukamy czy towar już jest w koszyku
        var istniejaca = koszyk.FirstOrDefault(p => p.TowarId == idTowaru);
        if (istniejaca != null)
        {
            istniejaca.Ilosc += ilosc;
        }
        else
        {
            koszyk.Add(new PozycjaKoszykaVM
            {
                TowarId = towar.IdTowaru,
                TowarNazwa = towar.Nazwa,
                CenaJednostkowa = towar.Cena,
                Ilosc = ilosc
            });
        }

        // Zapisz koszyk do sesji
        HttpContext.Session.SetObject("Koszyk", koszyk);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Usun(int towarId)
    {
        var koszyk = HttpContext.Session.GetObject<List<PozycjaKoszykaVM>>(SessionKey) ?? new();
        koszyk.RemoveAll(p => p.TowarId == towarId);
        HttpContext.Session.SetObject(SessionKey, koszyk);
        return RedirectToAction("Index");
    }
}




// Checking: EURUSD
// ✅ New data detected for EURUSD at 2025-05-29 16:45:03.733347
// Analyzing data at: 2025-05-29 16:45:03.763600 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:45:09.653617
// 🐂 Bull bias for EURUSD
// 📈 Buy signal detected
// 📊 Pullback bar: date           2025-05-29 17:30:00
// Open                       1.13492
// High                       1.13562
// Low                        1.13431
// Close                      1.13484
// tick_volume                   1507
// spread                           0
// real_volume                      0
// Name: 498, dtype: object
// 📊 Second bar: date           2025-05-29 17:15:00
// Open                       1.13568
// High                       1.13696
// Low                        1.13487
// Close                      1.13494
// tick_volume                   2017
// spread                           0
// real_volume                      0
// Name: 497, dtype: object

// 🔍 Checking: GBPUSD
// 📦 Dispatching signal for: EURUSD at time: 2025-05-29 16:45:10.001596
// ✅ New data detected for GBPUSD at 2025-05-29 16:45:10.052955
// Analyzing data at: 2025-05-29 16:45:10.108440 for ['M15', 'H1', 'H4', 'D1', 'W1']
// []
// ✅ Validation passed for EURUSD
// ✅ Order placed successfully for EURUSD: Order placed successfully
// Data analyzed at: 2025-05-29 16:45:16.169343
// 🐂 Bull bias for GBPUSD
// 🔕 No buy signal

// 🔍 Checking: USDJPY
// ✅ New data detected for USDJPY at 2025-05-29 16:45:16.446396
// Analyzing data at: 2025-05-29 16:45:16.488391 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:45:21.591169
// ❓ No bias detected

// 🔍 Checking: GER40
// ✅ New data detected for GER40 at 2025-05-29 16:45:21.800027
// Analyzing data at: 2025-05-29 16:45:21.814789 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:45:26.950513
// 🐂 Bull bias for GER40
// 🔕 No buy signal

// 🔍 Checking: AUDUSD
// ✅ New data detected for AUDUSD at 2025-05-29 16:45:27.163772
// Analyzing data at: 2025-05-29 16:45:27.208069 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:45:34.247595
// 🐂 Bull bias for AUDUSD
// 🔕 No buy signal

// 🔍 Checking: BTCUSD
// ✅ New data detected for BTCUSD at 2025-05-29 16:45:34.507289
// Analyzing data at: 2025-05-29 16:45:34.611012 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:45:40.818361
// 🐂 Bull bias for BTCUSD
// 🔕 No buy signal

// 🔍 Checking: XAUUSD
// ✅ New data detected for XAUUSD at 2025-05-29 16:45:41.161728
// Analyzing data at: 2025-05-29 16:45:41.190890 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:45:46.513065
// 🐂 Bull bias for XAUUSD
// 📈 Buy signal detected
// 📊 Pullback bar: date           2025-05-29 17:30:00
// Open                        3310.9
// High                       3312.58
// Low                        3304.71
// Close                      3308.01
// tick_volume                   4110
// spread                           8
// real_volume                      0
// Name: 498, dtype: object
// 📊 Second bar: date           2025-05-29 17:15:00
// Open                       3317.56
// High                       3317.92
// Low                        3309.98
// Close                      3310.93
// tick_volume                   3715
// spread                           9
// real_volume                      0
// Name: 497, dtype: object

// 🔍 Checking: USDCAD
// ✅ New data detected for USDCAD at 2025-05-29 16:45:46.949467
// Analyzing data at: 2025-05-29 16:45:46.955711 for ['M15', 'H1', 'H4', 'D1', 'W1']
// 📦 Dispatching signal for: XAUUSD at time: 2025-05-29 16:45:47.089888
// []
// ✅ Validation passed for XAUUSD
// ✅ Order placed successfully for XAUUSD: Order placed successfully
// Data analyzed at: 2025-05-29 16:45:52.843189
// ❓ No bias detected

// 🔍 Checking: SpotCrude
// ✅ New data detected for SpotCrude at 2025-05-29 16:45:53.077513
// Analyzing data at: 2025-05-29 16:45:53.157632 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:45:58.297641
// 🐻 Bear bias for SpotCrude
// 📉 Sell signal detected
// 📊 Pullback bar: date           2025-05-29 17:30:00
// Open                        61.051
// High                        61.211
// Low                         61.051
// Close                       61.141
// tick_volume                    400
// spread                          20
// real_volume                      0
// Name: 498, dtype: object
// 📊 Second bar: date           2025-05-29 17:15:00
// Open                        61.091
// High                        61.111
// Low                         60.881
// Close                       61.061
// tick_volume                    426
// spread                          20
// real_volume                      0
// Name: 497, dtype: object

// 🔍 Checking: US500
// 📦 Dispatching signal for: SpotCrude at time: 2025-05-29 16:45:58.730016
// ✅ New data detected for US500 at 2025-05-29 16:45:58.829213
// Analyzing data at: 2025-05-29 16:45:58.889616 for ['M15', 'H1', 'H4', 'D1', 'W1']
// []
// ✅ Validation passed for SpotCrude
// ✅ Order placed successfully for SpotCrude: Order placed successfully
// Data analyzed at: 2025-05-29 16:46:04.794310
// ❓ No bias detected

// 🔍 Checking: NAS100
// ✅ New data detected for NAS100 at 2025-05-29 16:46:05.151679
// Analyzing data at: 2025-05-29 16:46:05.221882 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:46:10.376249
// ❓ No bias detected

// 🔍 Checking: UK100
// ✅ New data detected for UK100 at 2025-05-29 16:46:10.656842
// Analyzing data at: 2025-05-29 16:46:10.766330 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:46:16.143685
// 🐂 Bull bias for UK100
// 🔕 No buy signal

// 🔍 Checking: ETHUSD
// ✅ New data detected for ETHUSD at 2025-05-29 16:46:16.294541
// Analyzing data at: 2025-05-29 16:46:16.320858 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:46:20.825825
// ❓ No bias detected

// 🔍 Checking: EURJPY
// ✅ New data detected for EURJPY at 2025-05-29 16:46:21.140918
// Analyzing data at: 2025-05-29 16:46:21.197078 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:46:26.185182
// ❓ No bias detected

// 🔍 Checking: CHFJPY
// ✅ New data detected for CHFJPY at 2025-05-29 16:46:26.366430
// Analyzing data at: 2025-05-29 16:46:26.408067 for ['M15', 'H1', 'H4', 'D1', 'W1']
// Data analyzed at: 2025-05-29 16:46:32.035326
// 🐂 Bull bias for CHFJPY
// 🔕 No buy signal

// 🌀 Main loop: 2025-05-29 16:46:42.177820

// 🔍 Check
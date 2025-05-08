using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models;

public class ZamowienieViewModel
{
    public int IdZamowienia { get; set; }

    [Required]
    public string UzytkownikId { get; set; } = "";

    [Required]
    public string Adres { get; set; } = "";

    [Required]
    [Display(Name = "Metoda płatności")]
    public string MetodaPlatnosci { get; set; } = "";

    [Required]
    [Display(Name = "Koszyk")]
    public int KoszykId { get; set; }
}

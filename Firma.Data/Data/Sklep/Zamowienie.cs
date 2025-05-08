using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Data.Data.Sklep
{
    public class Zamowienie
    {
        [Key]
        public int IdZamowienia { get; set; }

        public DateTime DataZamowienia { get; set; } = DateTime.UtcNow;

        [Required]
        public decimal Suma { get; set; }

        [Required]
        public string UzytkownikId { get; set; } = string.Empty;

        public Uzytkownik Uzytkownik { get; set; } = null!;

        [Required]
        public string Adres { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Metoda płatności")]
        public string MetodaPlatnosci { get; set; } = string.Empty;

        [Required]
        public int KoszykId { get; set; }

        public Koszyk Koszyk { get; set; } = null!;
    }
}

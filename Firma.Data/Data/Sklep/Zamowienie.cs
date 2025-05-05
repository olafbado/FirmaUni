using System.ComponentModel.DataAnnotations;

namespace Firma.Data.Data.Sklep
{
    public class Zamowienie
    {
        [Key]
        public int IdZamowienia { get; set; }

        public DateTime DataZamowienia { get; set; } = DateTime.UtcNow;

        [Required]
        public string ImieNazwisko { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Adres { get; set; } = string.Empty;

        [Required]
        public decimal Suma { get; set; }

        public ICollection<PozycjaZamowienia> Pozycje { get; set; } = new List<PozycjaZamowienia>();

        public string? UzytkownikId { get; set; }
        public Uzytkownik? Uzytkownik { get; set; }
    }
}

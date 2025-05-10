using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Data.Data.Sklep
{
    public class Towar
    {
        [Key]
        public int IdTowaru { get; set; }

        [Required(ErrorMessage = "Wpisz kod")]
        public required string Kod { get; set; }

        [Required(ErrorMessage = "Wpisz nazwę towaru")]
        public required string Nazwa { get; set; }

        [Required(ErrorMessage = "Wpisz cenę towaru")]
        [Column(TypeName = "money")]
        public decimal Cena { get; set; }

        [Required(ErrorMessage = "Dodaj zdjecie")]
        [Display(Name = "Wybierz zdjecie")]
        public required string FotoUrl { get; set; }

        public string Opis { get; set; } = string.Empty;

        [ForeignKey("Rodzaj")]
        public int IdRodzaju { get; set; }
        public Rodzaj? Rodzaj { get; set; }

        [Required(ErrorMessage = "Podaj ilość dostępną w magazynie")]
        [Range(0, int.MaxValue, ErrorMessage = "Ilość nie może być ujemna")]
        [Display(Name = "Ilość w magazynie")]
        public int Ilosc { get; set; } = 0;
    }
}

using System.ComponentModel.DataAnnotations;

namespace Firma.Data.Data.CMS
{
    public class Strona
    {
        [Key]
        public int IdStrony { get; set; }

        [Required(ErrorMessage = "Tytuł odnośnika jest wymagany")]
        [MaxLength(10, ErrorMessage = "Link może zawierac max 10 znakow")]
        [Display(Name = "Tytuł odnośnika")]
        public required string LinkTytul { get; set; }

        [Required(ErrorMessage = "Tytuł strony jest wymagany")]
        [MaxLength(50, ErrorMessage = "Tytuł może zawierac max 50 znakow")]
        [Display(Name = "Tytuł strony")]
        public required string Tytul { get; set; }

        [Display(Name = "Treść")]
        public required string Tresc { get; set; }

        [Display(Name = "Pozycja wyswietlania")]
        [Required(ErrorMessage = "Wpisz pozycje wyswietlania")]
        public int Pozycja { get; set; }
    }
}
